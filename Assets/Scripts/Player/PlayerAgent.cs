using System.Collections;
using UnityEngine;

public class PlayerAgent : MonoBehaviour
{
    public AudioSource bgm;
    public PlayerConfig playerConfig;
    public Animator weaponAnimator;
    public Animator fpCameraAnimator;
    public RaycastWeapon raycastWeapon;
    public Transform playerSight;
    public GameObject DeadPanel;
    public GameObject WonPanel;

    [SerializeField] private PlayerStateId initialState;

    internal bool isAiming = false;
    internal bool isCrouching = false;
    internal bool blockedControls = false;
    internal PlayerStateMachine stateMachine;
    internal StatesPlayerMovement playerMovement;
    internal PlayerSound playerSound;
    internal StatesPlayerMovement statesPlayerMovement;
    internal Transform cameraRoot;

    private void Start()
    {
        playerMovement = GetComponent<StatesPlayerMovement>();
        playerSound = GetComponentInChildren<PlayerSound>();
        statesPlayerMovement = GetComponent<StatesPlayerMovement>();
        cameraRoot = transform.GetChild(0);

        DeadPanel.SetActive(false);
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
        stateMachine.RegisterState(new LostGameState());
        stateMachine.RegisterState(new WonGameState());
    }

    internal void DisableAudioListener()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioListener>().enabled = false;
    }

    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GotObjective()
    {
        GetComponent<PlayerHealthController>().isDead = true;
        stateMachine.ChangeState(PlayerStateId.WonGame, isExceptional: false);
    }
}
