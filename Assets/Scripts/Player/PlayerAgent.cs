using UnityEngine;

public class PlayerAgent : MonoBehaviour
{
    public PlayerConfig playerConfig;
    public Animator weaponAnimator;
    public Animator fpCameraAnimator;

    [SerializeField] private PlayerStateId initialState;

    internal bool isAiming = false;
    internal PlayerStateMachine stateMachine;
    internal StatesPlayerMovement playerMovement;
    internal PlayerSound playerSound;

    private void Start()
    {
        weaponAnimator = GetComponent<Animator>();
        fpCameraAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<StatesPlayerMovement>();
        playerSound = GetComponentInChildren<PlayerSound>();

        stateMachine = new PlayerStateMachine(this);
        RegisterStates();
        stateMachine.ChangeState(initialState);
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
        stateMachine.RegisterState(new CrouchingState());
        stateMachine.RegisterState(new CrouchingWalkState());
        stateMachine.RegisterState(new AimingState());
        stateMachine.RegisterState(new WalkingAimingState());
    }
}
