using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float persecuteDistance = 5;
    public float timeToRecalculate = 1;
    public float giveUpTime = 5;
    public float chaseSpeed = 5;
    public float patrollingSpeed = 1.5f;
    public float deathImpulse = 5;
    public float checkWhenShotTime = 1f;
}
