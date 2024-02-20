using UnityEngine;

public class EnemyHitboxScript : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    public void Impact()
    {
        Debug.Log("Enemy Hit");
    }
}
