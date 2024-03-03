internal class RunningState : PlayerState
{
    public void Enter(PlayerAgent agent)
    {
        agent.playerSound.minVol = agent.playerConfig.sprintSoundVolume;
        agent.playerSound.maxVol = agent.playerConfig.sprintSoundVolume;

        if (agent.isAiming)
            agent.fpCameraAnimator.SetBool("isAiming", false);

        agent.weaponAnimator.SetBool("isWalking", false);
        agent.weaponAnimator.SetBool("isRunning", true);
        agent.weaponAnimator.SetBool("isCrouchingWalk", false);
        agent.weaponAnimator.SetBool("isAiming", false);
    }

    public void Exit(PlayerAgent agent)
    {
        
    }

    public PlayerStateId GetStateId()
    {
        return PlayerStateId.Running;
    }

    public void Update(PlayerAgent agent)
    {
        
    }
}