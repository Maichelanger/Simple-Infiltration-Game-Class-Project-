using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController charController;
    private PlayerAnimationsController animationScript;
    private Vector3 moveDirection;
    private Vector2 movementInput;
    //private float verticalVelocity = 0.0f;
    private float currentSpeed = 5.0f;
    //private float gravity = 9.8f;
    private PlayerControl playerControl;
    private PlayerData playerData;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        animationScript = GetComponent<PlayerAnimationsController>();
        playerControl = new PlayerControl();
        playerData = GetComponent<PlayerData>();
    }

    private void OnEnable()
    {
        playerControl.Enable();

        playerControl.InGame.Movement.performed += OnMovement;
        playerControl.InGame.Movement.canceled += OnMovement;
    }

    private void OnDisable()
    {
        playerControl.InGame.Movement.performed -= OnMovement;
        playerControl.InGame.Movement.canceled -= OnMovement;

        playerControl.Disable();
    }

    private void Update()
    {
        MovementCalculation();
        CheckInputs();
    }

    private void MovementCalculation()
    {
        moveDirection = new Vector3(movementInput.x, 0.0f, movementInput.y);

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= currentSpeed * Time.deltaTime;

        //ApplyGravity();

        charController.Move(moveDirection);
    }
    /*
    private void ApplyGravity()
    {
        if (!charController.isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity = 0.0f;
        }

        moveDirection.y = verticalVelocity;
    }
    */
    private void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void CheckInputs()
    {
        if (movementInput.x == 0 && movementInput.y == 0)
        {
            animationScript.UpdateStates("idle");
        }
        else if (animationScript.GetCurrentState() == "idle")
        {
            animationScript.UpdateStates("walk");
        }
    }

    public void ChangeSpeed(float newSpeed, string newAnimation)
    {
        currentSpeed = newSpeed;

        animationScript.UpdateStates(newAnimation);
    }
}