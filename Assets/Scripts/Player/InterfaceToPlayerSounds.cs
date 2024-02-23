using UnityEngine;

public class InterfaceToPlayerSounds : MonoBehaviour
{
    [SerializeField] private PlayerSound playerSound;

    public void PlayFootStepSound()
    {
        playerSound.PlayFootStepSound();
    }
}
