using UnityEngine;

public class DeadState : State
{
    internal Vector3 impactDirection;

    public void Enter(AiAgent agent)
    {
        agent.navAgent.enabled = false;
        agent.ragdoll.EnableRagdoll();
        agent.ragdoll.ApplyForce(impactDirection * agent.config.deathImpulse);

        agent.DestroyObject();
    }

    public void Exit(AiAgent agent)
    {

    }

    public StateId GetId()
    {
        return StateId.Dead;
    }

    public void Update(AiAgent agent)
    {

    }
}
