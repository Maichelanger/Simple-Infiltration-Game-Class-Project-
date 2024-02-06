using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunAndCrouch : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float crouchSpeed = 2f;

    private PlayerMovement playerMovement;
    private PlayerControl playerControl;
    private PlayerSound playerSound;
    private PlayerData playerData;
    private Transform cameraRoot;
    private float standHeight = 1.6f;
    private float crouchHeight = 1f;
    private float sprintSoundVolume = 1f;
    private float crouchSoundVolume = 0.1f;
    private float walkSoundMinVolume = 0.2f, walkSoundMaxVolume = 0.6f;
    private float walkStepDistance = 0.4f;
    private float sprintStepDistance = 0.25f;
    private float crouchStepDistance = 0.5f;

    private void Awake()
    {
        playerControl = new PlayerControl();
        playerMovement = GetComponent<PlayerMovement>();
        playerSound = GetComponentInChildren<PlayerSound>();
        cameraRoot = transform.GetChild(0);
        playerData = GetComponent<PlayerData>();
    }

    private void Start()
    {
        playerSound.minVol = walkSoundMinVolume;
        playerSound.maxVol = walkSoundMaxVolume;
        playerSound.stepDistance = walkStepDistance;
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
        if (!playerData.isCrouching && context.started)
        {
            playerMovement.ChangeSpeed(sprintSpeed, "run");

            playerSound.stepDistance = sprintStepDistance;
            playerSound.minVol = sprintSoundVolume;
            playerSound.maxVol = sprintSoundVolume;
        }
    }

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
}
