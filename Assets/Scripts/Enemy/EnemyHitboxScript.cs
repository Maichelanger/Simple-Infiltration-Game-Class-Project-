using UnityEngine;

public class EnemyHitboxScript : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    internal EnemyHealthController healthController;

    public void Impact(Vector3 impactDirection)
    {
        healthController.TakeDamage(damage, impactDirection);
    }
}
