public class LostGameState : PlayerState
{
    public void Enter(PlayerAgent agent)
    {
        agent.DeadPanel.SetActive(true);
        agent.blockedControls = true;
        agent.statesPlayerMovement.enabled = false;

        agent.StartCoroutine(agent.RestartGame());
    }

    public void Exit(PlayerAgent agent)
    {

    }

    public PlayerStateId GetStateId()
    {
        return PlayerStateId.LostGame;
    }

    public void Update(PlayerAgent agent)
    {

    }
}
