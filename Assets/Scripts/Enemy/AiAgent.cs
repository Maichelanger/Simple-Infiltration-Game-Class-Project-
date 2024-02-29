using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiAgentConfig config;
    public Transform[] patrollingTargets;

    [SerializeField] private StateId initialState;

    internal NavMeshAgent navAgent;
    internal EnemyRagdoll ragdoll;
    internal StateMachine stateMachine;
    internal FieldOfView fieldOfView;

    private bool inGivingUpCooldown = false;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<EnemyRagdoll>();
        fieldOfView = GetComponent<FieldOfView>();
        stateMachine = new StateMachine(this);
        RegisterStates();
        stateMachine.ChangeState(initialState);
    }

    void Update()
    {
        stateMachine.Update();

        if (fieldOfView.playerInSight)
        {
            stateMachine.ChangeState(StateId.ChasePlayer);
        }
        else if (!inGivingUpCooldown)
        {
            StartCoroutine(CheckPlayerBeforeGivingUp());
        }
    }

    private void RegisterStates()
    {
        stateMachine.RegisterState(new ChaseState());
        stateMachine.RegisterState(new DeadState());
        stateMachine.RegisterState(new PatrollingState());
    }

    internal void DestroyObject()
    {
        Destroy(gameObject, 5);
    }

    internal Vector3 GetCurrentPosition()
    {
        return transform.position;
    }

    IEnumerator CheckPlayerBeforeGivingUp()
    {
        inGivingUpCooldown = true;
        yield return new WaitForSeconds(config.giveUpTime);

        if (!fieldOfView.playerInSight)
        {
            stateMachine.ChangeState(StateId.Patrolling);
        }

        inGivingUpCooldown = false;
    }
}
