using SaiUtils.StateMachine;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyBaseState
{
    Transform target;
    NavMeshAgent navMeshAgent;
    float enemySpeedTempHolder;

    Vector2 _attackTimeRange = new Vector2(1f, 3f);
    float _attackTime;
    float _attackTimer;
    public EnemyAttackState(EnemyBrain controller, NavMeshAgent navMeshAgent) : base(controller) 
    {
        this.navMeshAgent = navMeshAgent;

        _attackTime = Random.Range(_attackTimeRange.x, _attackTimeRange.y);
        _attackTimer = 0;
    }

    public override void OnEnter()
    {
        enemySpeedTempHolder = navMeshAgent.speed;
        navMeshAgent.speed = 0;

        target = Controller.Target;

        Debug.Log("Attack State");
    }

    public override void Update()
    {
        Controller.transform.LookAt(target);

        _attackTimer += Time.deltaTime;
        if (_attackTimer >= _attackTime)
        {
            _attackTimer = 0;

            Controller.Fire();
        }
    }

    public override void OnExit()
    {
        navMeshAgent.speed = enemySpeedTempHolder;
    }


}