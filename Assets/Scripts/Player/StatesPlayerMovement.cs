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
        /*
        playerControl.InGame.Sprint.performed += OnWalk;
        playerControl.InGame.Sprint.canceled += OnWalk;
        playerControl.InGame.Crouch.performed += OnCrouch;
        */
    }

    private void OnDisable()
    {
        playerControl.InGame.Movement.performed -= OnMovement;
        playerControl.InGame.Movement.canceled -= OnMovement;
        playerControl.InGame.Sprint.started -= OnSprint;
        /*
        playerControl.InGame.Sprint.performed -= OnWalk;
        playerControl.InGame.Sprint.canceled -= OnWalk;
        playerControl.InGame.Crouch.performed -= OnCrouch;
        */

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
        if (agent.stateMachine.currentStateId == PlayerStateId.Idle)
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
            agent.stateMachine.ChangeState(PlayerStateId.Idle);
        }
        else if (agent.stateMachine.currentStateId == PlayerStateId.Idle)
        {
            agent.stateMachine.ChangeState(PlayerStateId.Walking);
        }
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        if ((agent.stateMachine.currentStateId == PlayerStateId.Crouching) && context.started)
        {
            //playerMovement.ChangeSpeed(sprintSpeed, "run");
            currentSpeed = agent.playerConfig.sprintSpeed;
            agent.stateMachine.ChangeState(PlayerStateId.Running);
        }
    }
    /*
    private void OnWalk(InputAction.CallbackContext context)
    {
        if (!playerData.isCrouching)
        {
            playerMovement.ChangeSpeed(walkSpeed, "walk");

            playerSound.stepDistance = walkStepDistance;
            playerSound.minVol = walkSoundMinVolume;
            playerSound.maxVol = walkSoundMaxVolume;
        }
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        if (playerData.isCrouching)
        {
            cameraRoot.localPosition = new Vector3(0f, standHeight, 0f);
            playerMovement.ChangeSpeed(walkSpeed, "walk");

            playerSound.stepDistance = walkStepDistance;
            playerSound.minVol = walkSoundMinVolume;
            playerSound.maxVol = walkSoundMaxVolume;

            playerData.isCrouching = false;
        }
        else
        {
            cameraRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
            playerMovement.ChangeSpeed(crouchSpeed, "crouch");

            playerSound.stepDistance = crouchStepDistance;
            playerSound.minVol = crouchSoundVolume;
            playerSound.maxVol = crouchSoundVolume;

            playerData.isCrouching = true;
        }
    }
    */
}
