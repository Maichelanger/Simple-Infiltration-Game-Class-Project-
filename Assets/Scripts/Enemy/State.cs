public enum StateId
{
    ChasePlayer,
    Dead,
    Patrolling
}

public interface State
{
    StateId GetId();
    void Enter(AiAgent agent);
    void Update(AiAgent agent);
    void Exit(AiAgent agent);
}
