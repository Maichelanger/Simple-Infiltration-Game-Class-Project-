using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimations : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        anim.SetFloat("speed", agent.velocity.magnitude);
    }
}
