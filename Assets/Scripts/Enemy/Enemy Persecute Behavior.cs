using UnityEngine;
using UnityEngine.AI;

public class EnemyPersecuteBehavior : MonoBehaviour
{
    [SerializeField] private float persecuteDistance = 5;
    [SerializeField] private float timeToRecalculate = 1;

    internal bool isPersecuting = false;

    private NavMeshAgent agent;
    private Transform player;
    private float timer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isPersecuting)
            PersecutePlayer();
    }

    public void PersecuteState()
    {
        agent.stoppingDistance = persecuteDistance;
        agent.SetDestination(player.position);
    }

    private void PersecutePlayer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            agent.SetDestination(player.position);

            timer = timeToRecalculate;
        }
    }
}
