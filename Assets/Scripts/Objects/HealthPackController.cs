using UnityEngine;

public class HealthPackController : MonoBehaviour
{
    [SerializeField] private float healthAmount = 25;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthController>().Heal(healthAmount);
            Destroy(gameObject);
        }
    }
}
