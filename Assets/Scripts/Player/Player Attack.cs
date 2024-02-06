using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Animator weaponAnimator;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 15f;

    private PlayerControl playerControl;
    private PlayerSound playerSound;
    private PlayerAnimationsController playerAnims;
    private Animator cameraAimAnim;
    private Camera mainCamera;
    private float nextFire = 0f;
    private Boolean canShoot = true;

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
            weaponAnimator.SetTrigger("shoot");
            Quaternion firePointCorrectedRotation = Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y + 90f, firePoint.rotation.z);
            Instantiate(bulletPrefab, firePoint.position, firePointCorrectedRotation);

            playerSound.PlayShootingSound();

            canShoot = false;
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
