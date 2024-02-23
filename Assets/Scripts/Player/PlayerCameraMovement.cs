using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerRoot, cameraRoot;
    [SerializeField] private bool invert;
    [SerializeField] private bool canUnlock = true;
    [SerializeField] private float sensitivity = 5f;
    [SerializeField] private int smoothSteps = 10;
    [SerializeField] private float smoothWeight = 0.4f;
    [SerializeField] private float rollAngle = 10f;
    [SerializeField] private float rollSpeed = 3f;
    [SerializeField] private Vector2 defaultLookLimits = new Vector2(-70f, 80f);
    [SerializeField] private Vector2 lookAngles;
    [SerializeField] private Vector2 currentMouseLook;
    [SerializeField] private Vector2 smoothMove;
    [SerializeField] private float currentRollAngle;
    [SerializeField] private int lastLookFrame;

    private PlayerControl playerControl;
    //private Vector2 mouseInput;

    private void Awake()
    {
        playerControl = new PlayerControl();
    }

    private void OnEnable()
    {
        playerControl.Enable();

        //playerControl.InGame.Camera.performed += OnLook;
        //playerControl.InGame.Camera.canceled += OnLook;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        LockAndUnlockCursor();

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    /**
    private void OnLook(InputAction.CallbackContext context)
    {
        currentMouseLook = context.ReadValue<Vector2>();
    }
    **/

    private void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private void LookAround()
    {
        currentMouseLook = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        lookAngles.x += currentMouseLook.x * sensitivity * (invert ? 1f : -1f);
        lookAngles.y += currentMouseLook.y * sensitivity;

        lookAngles.x = Mathf.Clamp(lookAngles.x, defaultLookLimits.x, defaultLookLimits.y);

        // Commented code is for having a rotating camera in Z when moving the mouse
        // currentRollAngle = Mathf.Lerp(currentRollAngle, Input.GetAxisRaw("Mouse X") * rollAngle, Time.deltaTime * 3f);

        cameraRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, currentRollAngle);
        playerRoot.localRotation = Quaternion.Euler(0f, lookAngles.y, 0f);
    }

}
