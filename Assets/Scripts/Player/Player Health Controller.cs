using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private HealthBarController healthBar;

    internal bool isDead = false;

    private int currentHealth;
    private PlayerAgent agent;

    private void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<PlayerAgent>();

        healthBar.healthBarSlider.maxValue = maxHealth;
        healthBar.UpdateValue(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        healthBar.UpdateValue(currentHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
            agent.stateMachine.ChangeState(PlayerStateId.LostGame, isExceptional: false);
        }
    }

    public void Heal(int healAmount)
    {
        if (isDead) return;

        currentHealth += healAmount;

        healthBar.UpdateValue(currentHealth);

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }   
}
