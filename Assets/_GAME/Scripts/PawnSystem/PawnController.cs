using System;
using System.Collections;
using SaiUtils.Extensions;
using SaiUtils.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public enum PawnState
{
    Idle,
    Move,
    Crouched,
    Frozen,
    Ambush,
    Attack
}

public class PawnController : MonoBehaviour
{
    [SerializeField] NavMeshAgent _navMeshAgent;
    [SerializeField] PawnAttackController _attackController;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public PawnAttackController AttackController => _attackController;

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

    StateMachine _pawnStateMachine;
    public PawnIdleState IdleState { get; private set; }
    public PawnMoveState MoveState { get; private set; }
    public PawnCrouchedState CrouchedState { get; private set; }
    public PawnFrozenState FrozenState { get; private set; }
    public PawnAmbushState AmbushState { get; private set; }
    public PawnAttackState AttackState { get; private set; }

    void Awake()
    {
        _pawnStateMachine = new StateMachine();

        IdleState = new PawnIdleState(this);
        MoveState = new PawnMoveState(this);
        CrouchedState = new PawnCrouchedState(this);
        FrozenState = new PawnFrozenState(this);
        AmbushState = new PawnAmbushState(this);
        AttackState = new PawnAttackState(this, _attackController);

        _pawnStateMachine.AddAnyTransition(IdleState, new FuncPredicate(() => _navMeshAgent.HasStoppedMoving()));
        _pawnStateMachine.AddAnyTransition(MoveState, new BlankPredicate());
        _pawnStateMachine.AddAnyTransition(CrouchedState, new BlankPredicate());
        _pawnStateMachine.AddAnyTransition(FrozenState, new BlankPredicate());
        _pawnStateMachine.AddAnyTransition(AmbushState, new BlankPredicate());
        _pawnStateMachine.AddAnyTransition(AttackState, new BlankPredicate());

        _pawnStateMachine.SetState(IdleState);
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
        Debug.Log($"Pawn speed set to {currentSpeed}");
    }

    [Button]
    public void Stop()
    {
        _navMeshAgent.ResetPath();
    }

    [Button]
    public void SetDestination(Vector2 coords)
    {
        Debug.Log("Setting destination");
        _pawnStateMachine.ChangeState(MoveState);
        _navMeshAgent.SetDestination(new Vector3(coords.x, transform.position.y, coords.y));
    }

    public void PushDestination(Vector2 coords)
    {
        Debug.Log("Pushing destination");
        SetPawnSpeed(_startingPawnSpeed * 2);
        _pawnStateMachine.ChangeState(MoveState);
        _navMeshAgent.SetDestination(new Vector3(coords.x, transform.position.y, coords.y));
    }

    public void ResetPawnSpeed() => SetPawnSpeed(_startingPawnSpeed);

    void OnTargetFound()
    {
        _pawnStateMachine.ChangeState(AttackState);
    }

    void Update()
    {
        _pawnStateMachine.Update();
        // Debug.Log($"Current speed: {currentSpeed}");
        _navMeshAgent.speed = currentSpeed;
    }

    void FixedUpdate()
    {
        _pawnStateMachine.FixedUpdate();   
    }
}
