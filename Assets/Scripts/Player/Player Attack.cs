using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Animator weaponAnimator;
    public float fireRate = 15f;

    private PlayerControl playerControl;
    private float nextFire = 0f;
    private Boolean canShoot = true;

    private void Awake()
    {
        playerControl = new PlayerControl();
    }

    private void OnEnable()
    {
        playerControl.Enable();

        playerControl.InGame.Shoot.performed += Shoot;
    }

    private void OnDisable()
    {
        playerControl.InGame.Shoot.performed -= Shoot;

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
        if (canShoot)
        {
            weaponAnimator.SetTrigger("shoot");

            canShoot = false;
        }
    }
}
