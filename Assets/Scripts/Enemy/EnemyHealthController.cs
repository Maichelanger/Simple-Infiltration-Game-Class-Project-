using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;

    internal bool isDead = false;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }
}
