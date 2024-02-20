using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void walk(bool walking)
    {
        anim.SetBool("isWalking", walking);
    }

    public void run(bool running)
    {
        anim.SetBool("isRunning", running);
    }

    public void attack()
    {
        anim.SetTrigger("attack");
    }

    public void die()
    {
        anim.SetTrigger("dead");
    }
}
