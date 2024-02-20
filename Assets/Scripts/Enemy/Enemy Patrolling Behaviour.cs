using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrollingBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] targets;

    private NavMeshAgent agent;
    private int currentTargetIndex;
    private Vector3 currentTarget;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    private void Update()
    {
        CheckDistanceToTarget();
    }

    private void CheckDistanceToTarget()
    {
        if(Vector3.Distance(transform.position, currentTarget) < 1f)
        {
            IterateTargets();
            UpdateDestination();
        }
    }

    private void UpdateDestination()
    {
        currentTarget = targets[currentTargetIndex].position;
        agent.SetDestination(currentTarget);
    }

    private void IterateTargets()
    {
        currentTargetIndex++;
        if (currentTargetIndex >= targets.Length)
        {
            currentTargetIndex = 0;
        }
    }
}
