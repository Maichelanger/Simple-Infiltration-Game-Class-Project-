using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public float minVol, maxVol;

    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioClip shootingSound;
    [SerializeField] private AudioClip winningSound;
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
        //CheckFootSteps();
    }

    //Obsolete: Footsteps are now handled through the animation events
    /*
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
    */

    public void PlayFootStepSound()
    {
        audioSource.volume = Random.Range(minVol,maxVol);
        AudioClip clip = footstepSounds[Random.Range(0,footstepSounds.Length)];
        audioSource.PlayOneShot(clip);
    }

    public void PlayShootingSound()
    {
        audioSource.PlayOneShot(shootingSound);
    }

    public void PlayWinningSound()
    {
        audioSource.PlayOneShot(winningSound);
    }
}
