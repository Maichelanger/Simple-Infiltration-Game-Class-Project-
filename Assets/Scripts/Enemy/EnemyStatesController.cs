using UnityEngine;
using UnityEngine.AI;

public class EnemyStatesController : MonoBehaviour
{
    private FieldOfView fov;
    private EnemyPatrollingBehaviour patrollingBehaviour;
    private EnemyPersecuteBehavior persecuteBehaviour;

    private void Start()
    {
        fov = GetComponent<FieldOfView>();
        patrollingBehaviour = GetComponent<EnemyPatrollingBehaviour>();
        persecuteBehaviour = GetComponent<EnemyPersecuteBehavior>();
    }

    private void Update()
    {
        if (fov.playerInSight)
        {
            patrollingBehaviour.isPatrolling = false;
            persecuteBehaviour.isPersecuting = true;
            persecuteBehaviour.PersecuteState();
        }
        else
        {
            persecuteBehaviour.isPersecuting = false;
            patrollingBehaviour.isPatrolling = true;
            patrollingBehaviour.PatrollingState();
        }
    }
}
