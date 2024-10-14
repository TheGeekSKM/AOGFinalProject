using SaiUtils.StateMachine;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCaughtState : EnemyBaseState
{
    NavMeshAgent _navMeshAgent;
    float _caughtTimer;
    float _caughtDuration = 3f;
    float _enemySpeedTempHolder;
    public EnemyCaughtState(EnemyBrain controller, NavMeshAgent navMeshAgent, float _caughtDuration) : base(controller) 
    {
        _navMeshAgent = navMeshAgent;
        this._caughtDuration = _caughtDuration;
    }

    public override void OnEnter()
    {
        _enemySpeedTempHolder = _navMeshAgent.speed;
        _navMeshAgent.speed = 0;

        Debug.Log("Caught State");
    }

    public override void Update()
    {
        _caughtTimer += Time.deltaTime;
        if (_caughtTimer >= _caughtDuration)
        {
            _caughtTimer = 0;
            Controller.EnemyBehavior.ChangeState(new EnemyWanderState(Controller, _navMeshAgent));
        }
    }

    public override void OnExit()
    {
        _navMeshAgent.speed = _enemySpeedTempHolder;
    }


}