public class PlayerStateMachine
{
    internal PlayerStateId currentStateId;

    private PlayerState[] states;
    private PlayerAgent agent;

    public PlayerStateMachine(PlayerAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(PlayerStateId)).Length;
        states = new PlayerState[numStates];
    }

    public void Update()
    {
        GetState(currentStateId)?.Update(agent);
    }

    public void RegisterState(PlayerState state)
    {
        int index = (int)state.GetStateId();
        states[index] = state;
    }

    public PlayerState GetState(PlayerStateId stateId)
    {
        int index = (int)stateId;
        return states[index];
    }

    public void ChangeState(PlayerStateId newStateId, bool isExceptional)
    {
        if (newStateId == currentStateId && !isExceptional)
        {
            return;
        }

        GetState(currentStateId)?.Exit(agent);
        currentStateId = newStateId;
        GetState(currentStateId)?.Enter(agent);
    }
}
