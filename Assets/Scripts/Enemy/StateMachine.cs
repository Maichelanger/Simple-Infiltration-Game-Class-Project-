public class StateMachine
{
    public State[] states;
    public AiAgent agent;
    public StateId currentStateId;

    public StateMachine(AiAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(StateId)).Length;
        states = new State[numStates];
    }

    public void RegisterState(State state)
    {
        int index = (int)state.GetId();
        states[index] = state;
    }

    public State GetState(StateId id)
    {
        int index = (int)id;
        return states[index];
    }

    public void Update()
    {
        GetState(currentStateId)?.Update(agent);
    }

    public void ChangeState(StateId newStateId)
    {
        if ((currentStateId == StateId.Dead) || (newStateId == currentStateId))
        {
            return;
        }

        GetState(currentStateId)?.Exit(agent);
        currentStateId = newStateId;
        GetState(currentStateId)?.Enter(agent);
    }
}
