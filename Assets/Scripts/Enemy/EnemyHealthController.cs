using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;

    internal bool isDead = false;

    private int currentHealth;
    private AiAgent agent;

    private void Start()
    {
        agent = GetComponent<AiAgent>();

        currentHealth = maxHealth;

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            EnemyHitboxScript hitbox = rigidBody.gameObject.AddComponent<EnemyHitboxScript>();
            hitbox.healthController = this;
        }
    }

    public void TakeDamage(int damage, Vector3 impactDirection)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true;
            Die(impactDirection);
        }
    }

    private void Die(Vector3 impactDirection)
    {
       DeadState deathState = agent.stateMachine.GetState(StateId.Dead) as DeadState;
       deathState.impactDirection = impactDirection;
       agent.stateMachine.ChangeState(StateId.Dead);
    }
}
