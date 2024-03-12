using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Objective collected");
            other.GetComponent<PlayerAgent>().GotObjective();
            Destroy(gameObject);
        }
    }
}
