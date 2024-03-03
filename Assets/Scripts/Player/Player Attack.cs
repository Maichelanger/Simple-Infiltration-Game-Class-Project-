using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Animator weaponAnimator;

    [SerializeField] private RaycastWeapon raycastWeapon;
    [SerializeField] private Transform playerSight;

    private PlayerControl playerControl;
    private PlayerSound playerSound;
    private PlayerAnimationsController playerAnims;

    private void Awake()
    {
        playerControl = new PlayerControl();
        playerSound = GetComponentInChildren<PlayerSound>();
        playerAnims = GetComponentInParent<PlayerAnimationsController>();
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

    private void Shoot(InputAction.CallbackContext context)
    {
        if (playerAnims.GetCurrentState() != "run")
        {
            if (raycastWeapon.canShoot)
                playerSound.PlayShootingSound();
            
            raycastWeapon.Shoot(playerSight);
        }
    }

    private void AimWeapon(InputAction.CallbackContext context)
    {
        bool compatibleState = (playerAnims.GetCurrentState() == "idle" || playerAnims.GetCurrentState() == "walk" || playerAnims.GetCurrentState() == "crouch");

        if (context.started && compatibleState)
        {
            playerAnims.UpdateStates("aim");
        }
        else
        {
            playerAnims.DisableAim();
        }
    }
}
