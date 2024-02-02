using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 20f;
    public float timeout = 5f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        LaunchProjectile();
        Invoke(nameof(DestroyObject), timeout);
    }

    private void OnTriggerEnter(Collider hitInfo)
    {
        if(hitInfo.CompareTag("EnemyHitbox"))
        {
            EnemyHitboxScript enemyHit = hitInfo.GetComponent<EnemyHitboxScript>();

            if(enemyHit != null)
            {
                enemyHit.Impact();
            }
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision hitInfo)
    {
        Destroy(gameObject);
    }

    private void LaunchProjectile()
    {
        rb.velocity = Camera.main.transform.forward * speed;

        transform.LookAt(transform.position + rb.velocity);

        transform.Rotate(Vector3.up, 90f);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
