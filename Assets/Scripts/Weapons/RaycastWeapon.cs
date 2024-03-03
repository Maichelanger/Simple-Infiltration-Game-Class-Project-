using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public Transform firePoint;
    public float fireRate = 15f;

    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private TrailRenderer bulletTrailEffect;

    internal bool canShoot = true;

    private float nextFire = 0f;
    private Ray shootingRay;
    private RaycastHit raycastHitInfo;

    private void Update()
    {
        CheckShootRate();
    }

    private void CheckShootRate()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            canShoot = true;
        }
    }

    public void Shoot(Transform target)
    {
        if (canShoot)
        {
            /*
            weaponAnimator.SetTrigger("shoot");
            Quaternion firePointCorrectedRotation = Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y + 90f, firePoint.rotation.z);
            Instantiate(bulletPrefab, firePoint.position, firePointCorrectedRotation);

            playerSound.PlayShootingSound();

            canShoot = false;
            */

            GenerateBullet(target);
            canShoot = false;
        }
    }

    private void GenerateBullet(Transform target)
    {
        muzzleFlash.Emit(1);

        shootingRay.origin = firePoint.position;
        shootingRay.direction = target.position - firePoint.position;

        var tracer = Instantiate(bulletTrailEffect, firePoint.position, Quaternion.identity);
        tracer.AddPosition(firePoint.position);
        if (Physics.Raycast(shootingRay, out raycastHitInfo))
        {
            hitEffect.transform.position = raycastHitInfo.point;
            hitEffect.transform.forward = raycastHitInfo.normal;
            hitEffect.Emit(1);

            tracer.transform.position = raycastHitInfo.point;

            var collidedRb = raycastHitInfo.collider.GetComponent<Rigidbody>();
            if (collidedRb != null)
            {
                collidedRb.AddForceAtPosition(shootingRay.direction * 20f, raycastHitInfo.point, ForceMode.Impulse);
            }

            var enemyHitbox = raycastHitInfo.collider.GetComponent<EnemyHitboxScript>();
            if (enemyHitbox != null)
            {
                enemyHitbox.Impact(shootingRay.direction);
            }
        }
    }
}
