using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Animator weaponAnimator;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 15f;

    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private Transform raycastTarget;
    [SerializeField] private TrailRenderer bulletTrailEffect;

    private PlayerControl playerControl;
    private PlayerSound playerSound;
    private PlayerAnimationsController playerAnims;
    private Animator cameraAimAnim;
    private Camera mainCamera;
    private float nextFire = 0f;
    private Boolean canShoot = true;
    private Ray shootingRay;
    private RaycastHit raycastHitInfo;

    private void Awake()
    {
        playerControl = new PlayerControl();
        playerSound = GetComponentInChildren<PlayerSound>();
        playerAnims = GetComponentInParent<PlayerAnimationsController>();
        cameraAimAnim = transform.Find("Viewpoint").transform.Find("FP Camera").GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        playerControl.Enable();

        playerControl.InGame.Shoot.performed += Shoot;
        playerControl.InGame.Aim.started += AimWeapon;
        playerControl.InGame.Aim.performed += AimWeapon;
        playerControl.InGame.Aim.canceled += AimWeapon;
    }

    private void OnDisable()
    {
        playerControl.InGame.Shoot.performed -= Shoot;
        playerControl.InGame.Aim.started -= AimWeapon;
        playerControl.InGame.Aim.performed -= AimWeapon;
        playerControl.InGame.Aim.canceled -= AimWeapon;

        playerControl.Disable();
    }

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

    private void Shoot(InputAction.CallbackContext context)
    {
        if (canShoot && (playerAnims.GetCurrentState() != "run"))
        {
            /*
            weaponAnimator.SetTrigger("shoot");
            Quaternion firePointCorrectedRotation = Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y + 90f, firePoint.rotation.z);
            Instantiate(bulletPrefab, firePoint.position, firePointCorrectedRotation);

            playerSound.PlayShootingSound();

            canShoot = false;
            */

            GenerateBullet();

            playerSound.PlayShootingSound();

            canShoot = false;

        }
    }

    private void GenerateBullet()
    {
        weaponAnimator.SetTrigger("shoot");
        muzzleFlash.Emit(1);

        shootingRay.origin = firePoint.position;
        shootingRay.direction = raycastTarget.position - firePoint.position;

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

    private void AimWeapon(InputAction.CallbackContext context)
    {
        bool compatibleState = (playerAnims.GetCurrentState() == "idle" || playerAnims.GetCurrentState() == "walk" || playerAnims.GetCurrentState() == "crouch");

        if(context.started && compatibleState)
        {
            playerAnims.UpdateStates("aim");
        }
        else
        {
            playerAnims.DisableAim();
        }
    }
}
