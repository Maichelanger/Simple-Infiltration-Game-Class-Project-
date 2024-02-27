using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;

    internal bool isDead = false;

    private int currentHealth;

    private void Start()
    {
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
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }
}
