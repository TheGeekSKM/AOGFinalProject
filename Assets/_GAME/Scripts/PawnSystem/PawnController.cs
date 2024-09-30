using System.Collections;
using SaiUtils.Extensions;
using SaiUtils.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public enum PawnState
{
    Idle,
    SlowMove,
    PushedMove,
    Crouched,
    Frozen,
    Ambush
}

public class PawnController : MonoBehaviour
{
    [SerializeField] NavMeshAgent _navMeshAgent;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    PawnState _state;
    public PawnState PawnState => _state;

    StateMachine _pawnStateMachine;
    public PawnIdleState IdleState { get; private set; }
    public PawnSlowMoveState SlowMoveState { get; private set; }
    public PawnPushedMoveState PushedMoveState { get; private set; }
    public PawnCrouchedState CrouchedState { get; private set; }
    public PawnFrozenState FrozenState { get; private set; }
    public PawnAmbushState AmbushState { get; private set; }

    void Awake()
    {
        _pawnStateMachine = new StateMachine();

        IdleState = new PawnIdleState(this);
        SlowMoveState = new PawnSlowMoveState(this);
        PushedMoveState = new PawnPushedMoveState(this);
        CrouchedState = new PawnCrouchedState(this);
        FrozenState = new PawnFrozenState(this);
        AmbushState = new PawnAmbushState(this);

        _pawnStateMachine.AddAnyTransition(IdleState, new BlankPredicate());
        _pawnStateMachine.AddAnyTransition(SlowMoveState, new BlankPredicate());
        _pawnStateMachine.AddAnyTransition(PushedMoveState, new BlankPredicate());
        _pawnStateMachine.AddAnyTransition(CrouchedState, new BlankPredicate());
        _pawnStateMachine.AddAnyTransition(FrozenState, new BlankPredicate());
        _pawnStateMachine.AddAnyTransition(AmbushState, new BlankPredicate());

        _pawnStateMachine.SetState(IdleState);
    }

    void OnValidate()
    {
        _navMeshAgent = gameObject.GetOrAdd<NavMeshAgent>();
    }

    void ChangeStateWithDelay(PawnBaseState state, float delay)
    {
        StartCoroutine(ChangeStateWithDelayCoroutine(state, delay));        
    }

    IEnumerator ChangeStateWithDelayCoroutine(PawnBaseState state, float delay)
    {
        yield return new WaitForSeconds(delay);
        _pawnStateMachine.ChangeState(state);
    }

    [Button]
    public void Stop()
    {
        _navMeshAgent.ResetPath();
        ChangeStateWithDelay(IdleState, 0.2f);
    }

    [Button]
    public void SetDestination(Vector2 coords)
    {
        Debug.Log("Setting destination");
        ChangeStateWithDelay(SlowMoveState, 0.2f);
        _navMeshAgent.SetDestination(new Vector3(coords.x, transform.position.y, coords.y));
    }

    void Update()
    {
        _pawnStateMachine.Update();

        // if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        // {
        //     Stop();
        // }
    }

    void FixedUpdate()
    {
        _pawnStateMachine.FixedUpdate();   
    }
}
