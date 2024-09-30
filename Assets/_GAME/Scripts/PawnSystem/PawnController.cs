using SaiUtils.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class PawnController : MonoBehaviour
{
    [SerializeField] NavMeshAgent _navMeshAgent;

    void OnValidate()
    {
        _navMeshAgent = gameObject.GetOrAdd<NavMeshAgent>();
    }


    [Button]
    public void Stop()
    {
        _navMeshAgent.ResetPath();
    }

    [Button]
    public void SetDestination(Vector2 coords)
    {
        _navMeshAgent.SetDestination(new Vector3(coords.x, transform.position.y, coords.y));
    }
}
