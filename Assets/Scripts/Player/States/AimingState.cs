internal class AimingState : PlayerState
{
    public void Enter(PlayerAgent agent)
    {
        agent.fpCameraAnimator.SetBool("isAiming", true);
        agent.weaponAnimator.SetBool("isAiming", true);
    }

    public void Exit(PlayerAgent agent)
    {
        agent.fpCameraAnimator.SetBool("isAiming", false);
        agent.weaponAnimator.SetBool("isAiming", false);
    }

    public PlayerStateId GetStateId()
    {
        return PlayerStateId.Aiming;
    }

    public void Update(PlayerAgent agent)
    {
        
    }
}