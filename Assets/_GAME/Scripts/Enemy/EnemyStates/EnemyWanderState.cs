using SaiUtils.StateMachine;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWanderState : EnemyBaseState
{
    float _wanderTimer;
    float _wanderDuration;
    float _wanderRadius;
    NavMeshAgent _navMeshAgent;

    public EnemyWanderState(EnemyBrain controller, NavMeshAgent navMeshAgent, float wanderDuration = 5f, float wanderRadius = 20f) : base(controller) 
    {
        _navMeshAgent = navMeshAgent;
        _wanderDuration = wanderDuration;
        _wanderRadius = wanderRadius;
    }

    public override void OnEnter()
    {
        Debug.Log("Wander State");
    }

    public override void Update()
    {
        _wanderTimer += Time.deltaTime;
        if (_wanderTimer >= _wanderDuration)
        {
            _wanderTimer = 0;
            Vector3 randomDirection = Random.insideUnitSphere * _wanderRadius;
            randomDirection += Controller.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, _wanderRadius, 1);
            Vector3 finalPosition = hit.position;            
            _navMeshAgent.SetDestination(finalPosition);
        }
    }
}