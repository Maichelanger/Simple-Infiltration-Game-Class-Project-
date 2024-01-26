using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunAndCrouch : MonoBehaviour
{
    public float sprintSpeed = 10f;
    public float crouchSpeed = 2f;

    private PlayerMovement playerMovement;
    private PlayerControl playerControl;
    private Transform cameraRoot;
    private float walkSpeed;
    private float standHeight = 1.6f;
    private float crouchHeight = 1f;
    private bool isCrouching = false;

    private void Awake()
    {
        playerControl = new PlayerControl();
        playerMovement = GetComponent<PlayerMovement>();
        cameraRoot = transform.GetChild(0);
        walkSpeed = playerMovement.walkSpeed;
    }

    private void OnEnable()
    {
        playerControl.Enable();

        playerControl.InGame.Sprint.started += OnSprint;
        playerControl.InGame.Sprint.performed += OnWalk;
        playerControl.InGame.Sprint.canceled += OnWalk;
        playerControl.InGame.Crouch.performed += OnCrouch;
    }

    private void OnDisable()
    {
        playerControl.InGame.Sprint.started -= OnSprint;
        playerControl.InGame.Sprint.performed -= OnWalk;
        playerControl.InGame.Sprint.canceled -= OnWalk;
        playerControl.InGame.Crouch.performed -= OnCrouch;

        playerControl.Disable();
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        if (!isCrouching)
        {
            playerMovement.walkSpeed = sprintSpeed;
        }
    }

    private void OnWalk(InputAction.CallbackContext context)
    {
        if (!isCrouching)
        {
            playerMovement.walkSpeed = walkSpeed;
        }
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        if(isCrouching)
        {
            cameraRoot.localPosition = new Vector3(0f, standHeight, 0f);
            playerMovement.walkSpeed = walkSpeed;

            isCrouching = false;
        }
        else
        {
            cameraRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
            playerMovement.walkSpeed = crouchSpeed;

            isCrouching = true;
        }
    }
}
