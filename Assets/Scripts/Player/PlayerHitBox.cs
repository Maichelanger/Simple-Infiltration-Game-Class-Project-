using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private PlayerHealthController healthController;

    public void Impact()
    {
        Debug.Log("Player hit");
        healthController.TakeDamage(damage);
    }
}
