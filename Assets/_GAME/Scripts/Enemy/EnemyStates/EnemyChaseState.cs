using SaiUtils.StateMachine;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{
    Transform _target;
    NavMeshAgent _navMeshAgent;
    public EnemyChaseState(EnemyBrain controller, NavMeshAgent navMeshAgent) : base(controller) 
    {
        _navMeshAgent = navMeshAgent;
    }

    public override void OnEnter()
    {
        Debug.Log("Chase State");
        _target = Controller.Target;
    }

    public override void Update()
    {
        _navMeshAgent.SetDestination(_target.position);
    }

}