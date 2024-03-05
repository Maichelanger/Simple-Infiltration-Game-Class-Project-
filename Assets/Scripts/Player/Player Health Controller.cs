using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    internal bool isDead = false;

    private int currentHealth;
    private PlayerAgent agent;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true;
            agent.stateMachine.ChangeState(PlayerStateId.LostGame, isExceptional: false);
        }
    }
}
