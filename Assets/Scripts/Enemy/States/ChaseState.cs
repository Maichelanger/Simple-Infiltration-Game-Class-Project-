using System.Collections;
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

        agent.weaponIk.SetTargetTransform(GameObject.FindGameObjectWithTag("Target").transform);
    }

    public void Exit(AiAgent agent)
    {
        agent.weaponIk.SetTargetTransform(null);
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

        ShootPlayer(agent);
    }

    private void ShootPlayer(AiAgent agent)
    {
        if(agent.fieldOfView.playerInSight)
        {
            Vector3 targetVector = GameObject.FindGameObjectWithTag("Target").transform.position + (Random.insideUnitSphere * agent.aimingInnacuracy);
            //How can I destroy the GameObject after 1 second?
            GameObject temp = new GameObject();
            Transform target = temp.transform;
            target.position = targetVector;
            agent.DestroyObject(temp);

            
            if (agent.weapon.GetComponent<RaycastWeapon>().canShoot)
                agent.sounds.PlayShootingSound();

            agent.weapon.GetComponent<RaycastWeapon>().Shoot(target);
        }
    }
}
