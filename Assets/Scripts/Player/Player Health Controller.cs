using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    internal bool isDead = false;

    private int currentHealth;
    //private PlayerController playerController;

    private void Start()
    {
        //playerController = GetComponent<PlayerController>();

        currentHealth = maxHealth;

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            PlayerHitBox hitbox = rigidBody.gameObject.AddComponent<PlayerHitBox>();
            hitbox.healthController = this;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
    }

    private void Die()
    {
        //playerController.Die(impactDirection);
    }
}
