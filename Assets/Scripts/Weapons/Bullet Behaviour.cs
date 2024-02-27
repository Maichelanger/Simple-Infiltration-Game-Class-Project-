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

    private void OnCollisionEnter(Collision hitInfo)
    {
        if(hitInfo.collider.CompareTag("Enemy"))
        {
            if(hitInfo.collider.TryGetComponent<EnemyHitboxScript>(out var enemyHit))
            {
                enemyHit.Impact(rb.velocity);
            }
        }

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
