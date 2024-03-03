using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiAgentConfig config;
    public PlayerSound sounds;
    public GameObject weapon;
    public Transform[] patrollingTargets;
    public float aimingInnacuracy = 0;

    [SerializeField] private StateId initialState;

    internal NavMeshAgent navAgent;
    internal EnemyRagdoll ragdoll;
    internal StateMachine stateMachine;
    internal FieldOfView fieldOfView;
    internal WeaponIk weaponIk;

    private bool inGivingUpCooldown = false;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<EnemyRagdoll>();
        fieldOfView = GetComponent<FieldOfView>();
        weaponIk = GetComponent<WeaponIk>();
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
        Destroy(weapon, 5);
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
