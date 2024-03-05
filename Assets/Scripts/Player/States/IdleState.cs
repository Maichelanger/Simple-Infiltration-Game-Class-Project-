internal class IdleState : PlayerState
{
    public void Enter(PlayerAgent agent)
    {
        if (agent.isAiming)
        {
            agent.stateMachine.ChangeState(PlayerStateId.Aiming, isExceptional: false);
            return;
        }

        /*
        agent.weaponAnimator.SetBool("isWalking", false);
        agent.weaponAnimator.SetBool("isRunning", false);
        agent.weaponAnimator.SetBool("isCrouchingWalk", false);
        agent.weaponAnimator.SetBool("isAiming", false);
        */
    }

    public void Exit(PlayerAgent agent)
    {
        
    }

    public PlayerStateId GetStateId()
    {
        return PlayerStateId.Idle;
    }

    public void Update(PlayerAgent agent)
    {
        
    }
}