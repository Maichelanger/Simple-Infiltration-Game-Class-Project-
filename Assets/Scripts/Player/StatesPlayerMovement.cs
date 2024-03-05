using UnityEngine;
using UnityEngine.InputSystem;

public class StatesPlayerMovement : MonoBehaviour
{
    private PlayerAgent agent;
    private CharacterController charController;
    private Vector3 moveDirection;
    private Vector2 movementInput;
    private float currentSpeed = 5.0f;
    private PlayerControl playerControl;

    private void Awake()
    {
        agent = GetComponent<PlayerAgent>();
        charController = GetComponent<CharacterController>();
        playerControl = new PlayerControl();
    }

    private void OnEnable()
    {
        playerControl.Enable();

        playerControl.InGame.Movement.performed += OnMovement;
        playerControl.InGame.Movement.canceled += OnMovement;
        playerControl.InGame.Sprint.started += OnSprint;
        playerControl.InGame.Sprint.performed += OnWalk;
        playerControl.InGame.Sprint.canceled += OnWalk;
        playerControl.InGame.Crouch.performed += OnCrouch;
        playerControl.InGame.Shoot.performed += Shoot;
        playerControl.InGame.Aim.started += AimWeapon;
        playerControl.InGame.Aim.performed += AimWeapon;
        playerControl.InGame.Aim.canceled += AimWeapon;
    }

    private void OnDisable()
    {
        playerControl.InGame.Movement.performed -= OnMovement;
        playerControl.InGame.Movement.canceled -= OnMovement;
        playerControl.InGame.Sprint.started -= OnSprint;
        playerControl.InGame.Sprint.performed -= OnWalk;
        playerControl.InGame.Sprint.canceled -= OnWalk;
        playerControl.InGame.Crouch.performed -= OnCrouch;
        playerControl.InGame.Shoot.performed -= Shoot;
        playerControl.InGame.Aim.started -= AimWeapon;
        playerControl.InGame.Aim.performed -= AimWeapon;
        playerControl.InGame.Aim.canceled -= AimWeapon;

        playerControl.Disable();
    }

    private void Update()
    {
        MovementCalculation();
        CheckInputs();
        SendSpeedToAnimator();
    }

    private void SendSpeedToAnimator()
    {
        if (agent.stateMachine.currentStateId == PlayerStateId.Idle || agent.stateMachine.currentStateId == PlayerStateId.Aiming)
            agent.weaponAnimator.SetFloat("Speed", 0);
        else
            agent.weaponAnimator.SetFloat("Speed", currentSpeed);
    }

    private void MovementCalculation()
    {
        moveDirection = new Vector3(movementInput.x, 0.0f, movementInput.y);

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= currentSpeed * Time.deltaTime;

        charController.Move(moveDirection);
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void CheckInputs()
    {
        if (movementInput.x == 0 && movementInput.y == 0)
        {
            agent.stateMachine.ChangeState(PlayerStateId.Idle, isExceptional: false);
        }
        else if (agent.stateMachine.currentStateId == PlayerStateId.Idle || agent.stateMachine.currentStateId == PlayerStateId.Aiming)
        {
            agent.stateMachine.ChangeState(PlayerStateId.Walking, isExceptional: false);
        }
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        if (!agent.isCrouching && context.started)
        {
            currentSpeed = agent.playerConfig.sprintSpeed;
            agent.stateMachine.ChangeState(PlayerStateId.Running, isExceptional: false);
        }
    }

    private void OnWalk(InputAction.CallbackContext context)
    {
        if (!agent.isCrouching)
        {
            currentSpeed = agent.playerConfig.walkSpeed;
            agent.stateMachine.ChangeState(PlayerStateId.Walking, isExceptional: true);
        }
        else if (agent.isCrouching)
        {
            currentSpeed = agent.playerConfig.crouchSpeed;
            agent.stateMachine.ChangeState(PlayerStateId.Walking, isExceptional: true);
        }
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        if (agent.isCrouching)
        {
            agent.cameraRoot.localPosition = new Vector3(0f, agent.playerConfig.standHeight, 0f);
            currentSpeed = agent.playerConfig.walkSpeed;
            agent.isCrouching = false;
        }
        else
        {
            agent.cameraRoot.localPosition = new Vector3(0f, agent.playerConfig.crouchHeight, 0f);
            currentSpeed = agent.playerConfig.crouchSpeed;
            agent.isCrouching = true;
        }
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if (agent.stateMachine.currentStateId != PlayerStateId.Running)
        {
            if (agent.raycastWeapon.canShoot)
                agent.playerSound.PlayShootingSound();

            agent.raycastWeapon.Shoot(agent.playerSight);
        }
    }

    private void AimWeapon(InputAction.CallbackContext context)
    {
        // Player can only aim when in Idle, Walking or Crouching.
        // When on Idle, the player does have a special state with an special animation.
        // When on Walking or Crouching, the player does not have a special state, since there won't be a special animation for it.

        if (agent.stateMachine.currentStateId == PlayerStateId.Running)
            return;

        if (context.started)
        {
            if (agent.isAiming)
                return;

            agent.isAiming = true;
            agent.fpCameraAnimator.SetBool("isAiming", true);

            if (agent.stateMachine.currentStateId == PlayerStateId.Idle)
            {
                agent.stateMachine.ChangeState(PlayerStateId.Aiming, isExceptional: false);
            }
        }
        else
        {
            if (!agent.isAiming)
                return;

            agent.isAiming = false;
            agent.fpCameraAnimator.SetBool("isAiming", false);

            if (agent.stateMachine.currentStateId == PlayerStateId.Aiming)
            {
                agent.stateMachine.ChangeState(PlayerStateId.Idle, isExceptional: false);
            }
        }
    }
}
