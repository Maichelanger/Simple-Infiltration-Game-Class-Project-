using UnityEngine;

public class PatrollingState : State
{
    private int currentTargetIndex;
    private Vector3 currentTarget;

    public void Enter(AiAgent agent)
    {
        agent.navAgent.stoppingDistance = 0f;
        agent.navAgent.speed = agent.config.patrollingSpeed;
        UpdateDestination(agent);
    }

    public void Exit(AiAgent agent)
    {

    }

    public StateId GetId()
    {
        return StateId.Patrolling;
    }

    public void Update(AiAgent agent)
    {
        CheckDistanceToTarget(agent);
    }

    internal void UpdateDestination(AiAgent agent)
    {
        currentTarget = agent.patrollingTargets[currentTargetIndex].position;
        agent.navAgent.SetDestination(currentTarget);
    }

    private void CheckDistanceToTarget(AiAgent agent)
    {
        if (Vector3.Distance(agent.GetCurrentPosition(), currentTarget) < 1f)
        {
            IterateTargets(agent);
            UpdateDestination(agent);
        }
    }

    private void IterateTargets(AiAgent agent)
    {
        currentTargetIndex++;

        if (currentTargetIndex >= agent.patrollingTargets.Length)
        {
            currentTargetIndex = 0;
        }
    }
}
