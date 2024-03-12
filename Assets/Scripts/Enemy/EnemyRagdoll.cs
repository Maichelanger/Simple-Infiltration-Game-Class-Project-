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

    public void EnableRagdoll(AiAgent agent)
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
        }

        agent.aiRb.isKinematic = true;

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

    public void ApplyForce(Vector3 force)
    {
        var rigidBody = animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidBody.AddForce(force, ForceMode.VelocityChange);
    }
}
