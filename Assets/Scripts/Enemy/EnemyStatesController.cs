using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatesController : MonoBehaviour
{
    [SerializeField] private float giveUpTime = 3;

    private NavMeshAgent agent;
    private FieldOfView fov;
    private EnemyPatrollingBehaviour patrollingBehaviour;
    private EnemyPersecuteBehavior persecuteBehaviour;
    private EnemyRagdoll ragdoll;
    private EnemyHealthController healthController;
    private bool inGivingUpCooldown = false;

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
        else if (!inGivingUpCooldown)
        {
            StartCoroutine(CheckPlayerBeforeGivingUp());
        } else if (healthController.isDead)
        {
            patrollingBehaviour.isPatrolling = false;
            persecuteBehaviour.isPersecuting = false;
        }
    }

    private void DeadState()
    {
        agent.isStopped = true;
        ragdoll.EnableRagdoll();
    }

    IEnumerator CheckPlayerBeforeGivingUp()
    {
        inGivingUpCooldown = true;
        yield return new WaitForSeconds(giveUpTime);

        if (!fov.playerInSight)
        {
            persecuteBehaviour.isPersecuting = false;
            patrollingBehaviour.isPatrolling = true;
            patrollingBehaviour.PatrollingState();
        }

        inGivingUpCooldown = false;
    }
}
