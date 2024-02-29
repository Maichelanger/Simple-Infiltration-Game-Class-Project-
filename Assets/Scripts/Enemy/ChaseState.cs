using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    private Transform player;
    private float timer;

    public void Enter(AiAgent agent)
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        agent.navAgent.stoppingDistance = agent.config.persecuteDistance;
        agent.navAgent.speed = agent.config.chaseSpeed;
        agent.navAgent.SetDestination(player.position);
    }

    public void Exit(AiAgent agent)
    {
    }

    public StateId GetId()
    {
        return StateId.ChasePlayer;
    }

    public void Update(AiAgent agent)
    {
        if (!agent.navAgent.enabled)
            return;

        timer -= Time.deltaTime;
        if (!agent.navAgent.hasPath)
        {
            agent.navAgent.SetDestination(player.position);
        }

        if (timer <= 0)
        {
            Vector3 direction = player.position - agent.transform.position;
            direction.y = 0;
            if (direction.sqrMagnitude > agent.config.persecuteDistance * agent.config.persecuteDistance)
            {
                if(agent.navAgent.pathStatus != NavMeshPathStatus.PathPartial)
                    agent.navAgent.SetDestination(player.position);
            }

            timer = agent.config.timeToRecalculate;
        }
    }
}
