using UnityEngine;

public class PlayerAgent : MonoBehaviour
{
    public PlayerConfig playerConfig;
    public Animator weaponAnimator;
    public Animator fpCameraAnimator;
    public RaycastWeapon raycastWeapon;
    public Transform playerSight;

    [SerializeField] private PlayerStateId initialState;

    internal bool isAiming = false;
    internal bool isCrouching = false;
    internal PlayerStateMachine stateMachine;
    internal StatesPlayerMovement playerMovement;
    internal PlayerSound playerSound;
    internal Transform cameraRoot;

    private void Start()
    {
        playerMovement = GetComponent<StatesPlayerMovement>();
        playerSound = GetComponentInChildren<PlayerSound>();
        cameraRoot = transform.GetChild(0);

        stateMachine = new PlayerStateMachine(this);
        RegisterStates();
        stateMachine.ChangeState(initialState, isExceptional: true);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void RegisterStates()
    {
        stateMachine.RegisterState(new IdleState());
        stateMachine.RegisterState(new WalkingState());
        stateMachine.RegisterState(new RunningState());
        stateMachine.RegisterState(new AimingState());
    }
}
