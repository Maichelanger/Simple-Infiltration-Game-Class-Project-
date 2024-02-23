using System.Collections;
using UnityEngine;

public class EnemyStatesController : MonoBehaviour
{
    [SerializeField] private float giveUpTime = 3;

    private FieldOfView fov;
    private EnemyPatrollingBehaviour patrollingBehaviour;
    private EnemyPersecuteBehavior persecuteBehaviour;
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
        }
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
