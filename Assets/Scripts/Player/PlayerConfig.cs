using UnityEngine;

[CreateAssetMenu()]
public class PlayerConfig : ScriptableObject
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float crouchSpeed = 2f;
    public float standHeight = 1.6f;
    public float crouchHeight = 1f;
    public float sprintSoundVolume = 1f;
    public float crouchSoundVolume = 0.1f;
    public float walkSoundMinVolume = 0.2f, walkSoundMaxVolume = 0.6f;

}
