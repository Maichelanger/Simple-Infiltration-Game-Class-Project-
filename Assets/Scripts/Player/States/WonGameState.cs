public class WonGameState : PlayerState
{
    public void Enter(PlayerAgent agent)
    {
        agent.bgm.Stop();
        agent.statesPlayerMovement.SendSpeedToAnimator(0);
        agent.playerSound.PlayWinningSound();
        agent.WonPanel.SetActive(true);
        agent.blockedControls = true;
        agent.statesPlayerMovement.enabled = false;

        agent.StartCoroutine(agent.RestartGame());
    }

    public void Exit(PlayerAgent agent)
    {

    }

    public PlayerStateId GetStateId()
    {
        return PlayerStateId.WonGame;
    }

    public void Update(PlayerAgent agent)
    {

    }
}
