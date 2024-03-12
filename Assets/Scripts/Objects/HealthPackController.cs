using System.Collections;
using UnityEngine;

public class HealthPackController : MonoBehaviour
{
    [SerializeField] private float healthAmount = 25;
    [SerializeField] AudioClip healthPackSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(healthPackSound);
            other.GetComponent<PlayerHealthController>().Heal(healthAmount);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            StartCoroutine(DestroyHealthPack());
        }
    }

    IEnumerator DestroyHealthPack()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
