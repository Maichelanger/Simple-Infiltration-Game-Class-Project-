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
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
        patrollingBehaviour = GetComponent<EnemyPatrollingBehaviour>();
        persecuteBehaviour = GetComponent<EnemyPersecuteBehavior>();
        ragdoll = GetComponent<EnemyRagdoll>();
        healthController = GetComponent<EnemyHealthController>();
    }

    private void Update()
    {
        if (healthController.isDead)
        {
            patrollingBehaviour.isPatrolling = false;
            persecuteBehaviour.isPersecuting = false;
            DeadState();
        }else if (fov.playerInSight)
        {
            patrollingBehaviour.isPatrolling = false;
            persecuteBehaviour.isPersecuting = true;
            persecuteBehaviour.PersecuteState();
        }
        else if (!inGivingUpCooldown)
        {
            StartCoroutine(CheckPlayerBeforeGivingUp());
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
