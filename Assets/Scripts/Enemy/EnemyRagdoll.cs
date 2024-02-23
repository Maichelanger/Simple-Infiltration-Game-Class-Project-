using UnityEngine;

public class EnemyRagdoll : MonoBehaviour
{
    private Rigidbody[] rigidbodies;
    private Animator animator;

    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();

        DisableRagdoll();
    }

    public void EnableRagdoll()
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
        }

        animator.enabled = false;
    }

    private void DisableRagdoll()
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }

        animator.enabled = true;
    }
}