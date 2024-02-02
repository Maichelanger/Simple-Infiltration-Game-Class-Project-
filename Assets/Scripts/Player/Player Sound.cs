using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioClip shootingSound;
    [HideInInspector] public float minVol,maxVol;
    [HideInInspector] public float stepDistance;

    private AudioSource audioSource;
    private CharacterController charController;
    private float timeSinceLastSetp;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        charController = GetComponentInParent<CharacterController>();
    }

    private void Update()
    {
        CheckFootSteps();
    }

    private void CheckFootSteps()
    {
        if(!charController.isGrounded) return;

        if(charController.velocity.sqrMagnitude > 0)
        {
            timeSinceLastSetp += Time.deltaTime;
            if(timeSinceLastSetp > stepDistance)
            {
                timeSinceLastSetp = 0;
                PlayFootStepSound();
            }
        }
        else
        {
            timeSinceLastSetp = 0;
        }
    }

    private void PlayFootStepSound()
    {
        audioSource.volume = Random.Range(minVol,maxVol);
        AudioClip clip = footstepSounds[Random.Range(0,footstepSounds.Length)];
        audioSource.PlayOneShot(clip);
    }

    public void PlayShootingSound()
    {
        audioSource.PlayOneShot(shootingSound);
    }
}
