using System;
using UnityEngine;

[System.Serializable]
public class HumanBone
{
    public HumanBodyBones bone;
    public float boneWeight = 1.0f;
}

public class WeaponIk : MonoBehaviour
{
    [SerializeField] private Transform aimTransform;
    [SerializeField] private float angleLimit = 90.0f;
    [SerializeField] private float distanceLimit = 1.5f;
    [SerializeField] private int aimIterations = 10;
    [SerializeField][Range(0, 1)] private float aimWeight = 1.0f;
    [SerializeField] private HumanBone[] humanBones;

    private Transform[] boneTransforms;
    private Transform targetTransform;

    private void Start()
    {
        Animator animator = GetComponent<Animator>();
        boneTransforms = new Transform[humanBones.Length];
        for (int i = 0; i < humanBones.Length; i++)
        {
            boneTransforms[i] = animator.GetBoneTransform(humanBones[i].bone);
        }
    }

    private void LateUpdate()
    {
        if(targetTransform == null || aimTransform == null)
        {
            return;
        }

        Vector3 targetPos = GetTargetPos();

        for (int i = 0; i < aimIterations; i++)
        {
            for (int j = 0; j < boneTransforms.Length; j++)
            {
                Transform bone = boneTransforms[j];
                float boneWeight = humanBones[j].boneWeight * aimWeight;
                AimAtTarget(bone, targetPos, aimWeight);
            }
        }
    }

    public void SetTargetTransform(Transform target)
    {
        targetTransform = target;
    }

    public void SetAimTransform(Transform aim)
    {
        aimTransform = aim;
    }

    private void AimAtTarget(Transform bone, Vector3 targetPosition, float animationWeight)
    {
        Vector3 aimDirection = aimTransform.forward;
        Vector3 targetDirection = targetPosition - aimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, animationWeight);
        bone.rotation = blendedRotation * bone.rotation;
    }

    private Vector3 GetTargetPos()
    {
        Vector3 targetDirection = targetTransform.position - aimTransform.position;
        Vector3 aimDirection = aimTransform.forward;
        float blendOut = 0.0f;

        float targetAngle = Vector3.Angle(aimDirection, targetDirection);
        if (targetAngle > angleLimit)
        {
            blendOut += (targetAngle - angleLimit) / 50.0f;
        }

        float targetDistance = Vector3.Distance(targetTransform.position, aimTransform.position);
        if (targetDistance > distanceLimit)
        {
            blendOut += targetDistance - distanceLimit;
        }

        Vector3 direction = Vector3.Slerp(targetDirection, aimDirection, blendOut);
        return aimTransform.position + direction;
    }
}
