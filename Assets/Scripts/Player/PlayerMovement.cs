using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;

    private CharacterController charController;
    private Vector3 moveDirection;
    private Vector2 movementInput;
    private float gravity = 2f;
    private float verticalVelocity = 0.0f;
    private PlayerControl playerControl;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        playerControl = new PlayerControl();
    }

    private void OnEnable()
    {
        playerControl.Enable();

        playerControl.InGame.Movement.performed += OnMovement;
        playerControl.InGame.Movement.canceled += OnMovement;

        //playerControl.InGame.Shoot.performed += OnShoot;
        //playerControl.InGame.Shoot.canceled += OnShoot;
    }

    private void Update()
    {
        MovementCalculation();
    }

    private void FixedUpdate()
    {

    }

    private void MovementCalculation()
    {
        moveDirection = new Vector3(movementInput.x, 0.0f, movementInput.y);

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed * Time.deltaTime;

        ApplyGravity();

        charController.Move(moveDirection);
    }

    private void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;

        moveDirection.y = verticalVelocity * Time.deltaTime;
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    /**
    private void OnShoot(InputAction.CallbackContext context)
    {
        Debug.Log("Shoot");
    }
    **/
}
