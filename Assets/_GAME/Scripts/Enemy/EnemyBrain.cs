using System.Collections;
using SaiUtils.StateMachine;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] int _enemyIndex = -1;
    public int EnemyIndex => _enemyIndex;

    [SerializeField] NavMeshAgent _navMeshAgent;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    [SerializeField] Transform _target;
    public Transform Target => _target;


    [Header("State Machine Variables")]
    [SerializeField] float _wanderRange = 20f;
    [SerializeField] float _wanderDuration = 5f;

    [Header("FOV Variables")]
    [SerializeField] LayerMask _targetLayer;
    [SerializeField] LayerMask _obstructionMask;
    [SerializeField] float radius;
    public float Radius => radius;
    [SerializeField] float angle;
    public float Angle => angle;
    GameObject pawn;
    bool canCheckFOV = true;
    public bool CanCheckFOV
    {
        get => canCheckFOV;
        set => canCheckFOV = value;
    }
    Coroutine fovRoutine;

    StateMachine _enemyBehavior = new StateMachine();
    public StateMachine EnemyBehavior => _enemyBehavior;

    public EnemyWanderState EnemyWanderState {get; private set;}
    // EnemyCaughtState enemyCaughtState;
    public EnemyAttackState EnemyAttackState {get; private set;}
    public EnemyChaseState EnemyChaseState {get; private set;}


    void Awake()
    {
        ConfigureStateMachine();
    }
    void ConfigureStateMachine()
    {
        EnemyWanderState = new EnemyWanderState(this, _navMeshAgent, _wanderDuration, _wanderRange);
        _enemyBehavior.AddAnyTransition(EnemyWanderState, new FuncPredicate(() => Target == null));

        // enemyCaughtState = new EnemyCaughtState(this, _navMeshAgent, 3f);
        // _enemyBehavior.AddAnyTransition(enemyCaughtState, new BlankPredicate());
        EnemyAttackState = new EnemyAttackState(this, _navMeshAgent);
        EnemyChaseState = new EnemyChaseState(this, _navMeshAgent);

        _enemyBehavior.AddTransition(EnemyChaseState, EnemyAttackState, new FuncPredicate(() => false));
        _enemyBehavior.AddTransition(EnemyWanderState, EnemyChaseState, new FuncPredicate(() => false));

        _enemyBehavior.SetState(EnemyWanderState);
    }
    
    void Start()
    {
        pawn = PawnController.Instance.gameObject;

        if (canCheckFOV) fovRoutine = StartCoroutine(FOVRoutine());
    }

    public void AttackTriggerEnter(GameObject other)
    {
        _enemyBehavior.ChangeState(EnemyAttackState);

    }

    public void AttackTriggerExit(GameObject other)
    {
        _enemyBehavior.ChangeState(EnemyChaseState);
    }

    public void ChaseTriggerEnter(GameObject other)
    {
        _target = other.transform;
        _enemyBehavior.ChangeState(EnemyChaseState);

    }

    public void ChaseTriggerExit()
    {
        _target = null;
        _enemyBehavior.ChangeState(EnemyWanderState);
    }

    public void SetEnemyIndex(int index)
    {
        _enemyIndex = index;
    }

    public void Fire()
    {

    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, _targetLayer);

        if (rangeChecks.Length > 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructionMask))
                {
                    ChaseTriggerEnter(target.gameObject);
                }
            }
        }
    }

    void Update()
    {
        _enemyBehavior.Update();

    }

    void FixedUpdate()
    {
        _enemyBehavior.FixedUpdate();
    }
}