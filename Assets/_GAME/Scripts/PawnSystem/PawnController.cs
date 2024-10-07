using System;
using System.Collections;
using System.Collections.Generic;
using SaiUtils.Extensions;
using SaiUtils.Singleton;
using SaiUtils.StateMachine;
using SaiUtils.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public enum PawnState
{
    Idle,
    Move,
    Frozen,
    Ambush,
    Attack
}

public class PawnController : Singleton<PawnController>
{
    [Header("Components")]
    [SerializeField] NavMeshAgent _navMeshAgent;
    [SerializeField] PawnAttackController _attackController;
    [SerializeField] TriggerController _destinationTrigger;
    [SerializeField] GrowingTriggerController _enemyScoutTrigger;
    [SerializeField] Health _health;
    [SerializeField] LootContainer _inventory;
    [SerializeField] Transform _firePoint;

    [Header("Settings")]
    [SerializeField] float _scoutSearchRange = 10f;
    [SerializeField] float _healthPerSecondWhenResting = 1f;

    [Header("Loot Nearby")]
    [SerializeField] List<LootContainer> _lootContainers;


    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public PawnAttackController AttackController => _attackController;
    public Health Health => _health;
    public LootContainer Inventory => _inventory;
    public Transform FirePoint => _firePoint;

    [SerializeField] float _startingPawnSpeed = 3f;

    PawnState _state;
    public PawnState PawnState 
    {
        get => _state;
        set
        {
            _state = value;
            Debug.Log($"Pawn state changed to {_state}");
        }
    }

    float currentSpeed;
    float restingCounter = 0;
    bool _resting = false;
    public bool IsResting 
    {
        get => _resting;
        set
        {
            _resting = value;
            Debug.Log($"Pawn is resting: {_resting}");
        }
    }

    StateMachine _pawnStateMachine;
    public PawnIdleState IdleState { get; private set; }
    private PawnMoveState MoveState { get; set; }
    public PawnFrozenState FrozenState { get; private set; }
    public PawnAmbushState AmbushState { get; private set; }
    public PawnAttackState AttackState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        _pawnStateMachine = new StateMachine();

        IdleState = new PawnIdleState(this);
        MoveState = new PawnMoveState(this);
        FrozenState = new PawnFrozenState(this);
        AmbushState = new PawnAmbushState(this);
        AttackState = new PawnAttackState(this, _attackController);

        _pawnStateMachine.AddAnyTransition(IdleState, new BlankPredicate());
        _pawnStateMachine.AddAnyTransition(MoveState, new BlankPredicate());
        _pawnStateMachine.AddAnyTransition(FrozenState, new BlankPredicate());
        _pawnStateMachine.AddAnyTransition(AmbushState, new BlankPredicate());
        _pawnStateMachine.AddAnyTransition(AttackState, new BlankPredicate());

        _pawnStateMachine.SetState(IdleState);
    }

    bool IsPlayerIdle()
    {
        if (!_navMeshAgent.HasStoppedMoving()) return false;
        if (PawnState == PawnState.Ambush) return false;
        if (PawnState == PawnState.Frozen) return false;
        if (PawnState == PawnState.Attack) return false;
        return true;
    }

    void OnEnable()
    {
        _attackController.OnTargetFound += OnTargetFound;
    }

    void OnDisable()
    {
        _attackController.OnTargetFound -= OnTargetFound;
    }

    void Start()
    {
        _navMeshAgent.speed = _startingPawnSpeed;
    }

    void OnValidate()
    {
        _navMeshAgent = gameObject.GetOrAdd<NavMeshAgent>();
    }

    public void SetPawnSpeed(float speed)
    {
        currentSpeed = speed;
        // Debug.Log($"Pawn speed set to {currentSpeed}");
    }

  
    [Button]
    public void Stop()
    {
        _navMeshAgent.ResetPath();
        PawnChatManager.Instance.AddChat("Ooop-freezin' in place, cap...", ChatterType.Pawn);
    }

    [Button]
    public void SetDestination(Vector2 coords)
    {
        Debug.Log("Setting destination");
        _pawnStateMachine.ChangeState(MoveState);
        _navMeshAgent.SetDestination(new Vector3(coords.x, transform.position.y, coords.y));
        PawnChatManager.Instance.AddChat($"Movin' to {coords.x}, {coords.y}, cap...might take me a sec...", ChatterType.Pawn);
    }

    public void PushDestination(Vector2 coords)
    {
        Debug.Log("Pushing destination");
        SetPawnSpeed(_startingPawnSpeed * 2);
        _pawnStateMachine.ChangeState(MoveState);
        _navMeshAgent.SetDestination(new Vector3(coords.x, transform.position.y, coords.y));
        PawnChatManager.Instance.AddChat($"Haulin' ass to {coords.x}, {coords.y}, cap!", ChatterType.Pawn);
    }

    public void ScoutAhead(Vector2 coords)
    {
        Debug.Log("Scouting ahead");
        SetPawnSpeed(_startingPawnSpeed / 2);

        CreateScoutTrigger(coords);

        _navMeshAgent.SetDestination(new Vector3(coords.x, transform.position.y, coords.y));
        PawnChatManager.Instance.AddChat($"Imma scout ahead, cap...gimme a sec.", ChatterType.Pawn);
    }

    void CreateScoutTrigger(Vector2 coords)
    {
        // create a destination trigger
        var trigger = Instantiate(_destinationTrigger, new Vector3(coords.x, transform.position.y, coords.y), Quaternion.identity);
        
        // add a listener to the trigger for when it is entered
        trigger.AddListener(TriggerEventType.Enter, (other) =>
        {
            Debug.Log("Scout ahead trigger entered"); // log that the trigger was entered
            _navMeshAgent.ResetPath(); // reset the path of the nav mesh agent
            ResetPawnSpeed(); // reset the pawn speed
            var scout = Instantiate(_enemyScoutTrigger, new Vector3(coords.x, transform.position.y, coords.y), Quaternion.identity); // create a scout trigger
            scout.Initialize(_scoutSearchRange, 10f); // initialize the scout trigger so that it grows to the search range
            Destroy(trigger.gameObject); // destroy the destination trigger
        });
    }

    public void SetCrouch(bool crouch)
    {
        if (crouch)
        {
            SetPawnSpeed(_startingPawnSpeed / 2);
        }
        else
        {
            ResetPawnSpeed();
        }
        PawnChatManager.Instance.AddChat($"Crouchin' down, cap...", ChatterType.Pawn);
    }

    public void SetPlayerRest() 
    {
        _pawnStateMachine.ChangeState(FrozenState);
        _resting = true;

        PawnChatManager.Instance.AddChat("Thanks cap...Restin' up...phew...", ChatterType.Pawn);
    }

    public void SetPlayerIdle()
    {
        _pawnStateMachine.ChangeState(IdleState);
        ResetPawnSpeed();
        PawnChatManager.Instance.AddChat("Alrighty, lemme know if you need me to do anythin', cap...", ChatterType.Pawn);
    }

    public void ResetPawnSpeed() => SetPawnSpeed(_startingPawnSpeed);

    public void AddLootContainer(GameObject lootContainer)
    {
        _lootContainers.Add(lootContainer.GetComponent<LootContainer>());
    }

    public void RemoveLootContainer(GameObject lootContainer)
    {
        _lootContainers.Remove(lootContainer.GetComponent<LootContainer>());
    }

    public void Loot()
    {
        for (int i = 0; i < _lootContainers.Count; i++)
        {
            _inventory.AddLootList(_lootContainers[i].Items);
            Destroy(_lootContainers[i].gameObject);
        }
    }

    void OnTargetFound()
    {
        _pawnStateMachine.ChangeState(AttackState);
    }

    void Update()
    {
        // _pawnStateMachine.Update();
        if (IsPlayerIdle())
        {
            _pawnStateMachine.ChangeState(IdleState);
            ResetPawnSpeed();
        }

        if (_resting)
        {
            Stop();
            restingCounter += Time.deltaTime;

            if (restingCounter >= 3)
            {
                _health.ChangeHealth(_healthPerSecondWhenResting);
                restingCounter = 0;
            }
        }

        _navMeshAgent.speed = currentSpeed;
    }

    void FixedUpdate()
    {
        _pawnStateMachine.FixedUpdate();   
    }
}
