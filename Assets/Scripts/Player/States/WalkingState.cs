internal class WalkingState : PlayerState
{
    public void Enter(PlayerAgent agent)
    {
        if (agent.isCrouching)
            CrouchWalk(agent);
        else
            RegularWalk(agent);
    }

    public void Exit(PlayerAgent agent)
    {
        
    }

    public PlayerStateId GetStateId()
    {
        return PlayerStateId.Walking;
    }

    public void Update(PlayerAgent agent)
    {
        
    }

    private void RegularWalk(PlayerAgent agent)
    {
        agent.playerSound.minVol = agent.playerConfig.walkSoundMinVolume;
        agent.playerSound.maxVol = agent.playerConfig.walkSoundMaxVolume;

        if (agent.isAiming)
            agent.fpCameraAnimator.SetBool("isAiming", true);

        /*
        agent.weaponAnimator.SetBool("isWalking", true);
        agent.weaponAnimator.SetBool("isRunning", false);
        agent.weaponAnimator.SetBool("isCrouchingWalk", false);
        agent.weaponAnimator.SetBool("isAiming", false);
        */
    }

    private void CrouchWalk(PlayerAgent agent)
    {
        agent.playerSound.minVol = agent.playerConfig.crouchSoundVolume;
        agent.playerSound.maxVol = agent.playerConfig.crouchSoundVolume;

        if (agent.isAiming)
            agent.fpCameraAnimator.SetBool("isAiming", true);

        /*
        agent.weaponAnimator.SetBool("isWalking", false);
        agent.weaponAnimator.SetBool("isRunning", false);
        agent.weaponAnimator.SetBool("isCrouchingWalk", true);
        agent.weaponAnimator.SetBool("isAiming", false);
        */
    }
}