using UnityEngine;

public class DeadState : State
{
    internal Vector3 impactDirection;

    public void Enter(AiAgent agent)
    {
        agent.navAgent.enabled = false;
        agent.ragdoll.EnableRagdoll();
        agent.ragdoll.ApplyForce(impactDirection * agent.config.deathImpulse);

        agent.weapon.GetComponent<Collider>().enabled = true;
        agent.weapon.GetComponent<Rigidbody>().isKinematic = false;
        agent.weapon.transform.SetParent(null);

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
