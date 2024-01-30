using UnityEngine;

public class PlayerAnimationsController : MonoBehaviour
{
    public Animator weaponAnimator;

    private bool isIdle = false;
    private bool isWalking = false;
    private bool isRunning = false;
    private bool isCrouching = false;

    public void UpdateStates(string newState)
    {   
        switch (newState.ToLower())
        {
            case "walk":
                OnWalk();
                break;
            case "run":
                OnSprint();
                break;
            case "crouch":
                OnCrouchWalk();
                break;
            case "idle":
                OnIdle();
                break;
        }
    }

    private void OnWalk()
    {
        if (isWalking) return;

        AllToFalse();

        isWalking = true;
        weaponAnimator.SetBool("isWalking", true);
        weaponAnimator.SetBool("isRunning", false);
        weaponAnimator.SetBool("isCrouchingWalk", false);
    }

    private void OnSprint()
    {
        if (isRunning) return;

        AllToFalse();

        isRunning = true;
        weaponAnimator.SetBool("isWalking", false);
        weaponAnimator.SetBool("isRunning", true);
        weaponAnimator.SetBool("isCrouchingWalk", false);
    }

    private void OnCrouchWalk()
    {
        if (isCrouching) return;

        AllToFalse();

        isCrouching = true;
        weaponAnimator.SetBool("isWalking", false);
        weaponAnimator.SetBool("isRunning", false);
        weaponAnimator.SetBool("isCrouchingWalk", true);
    }

    private void OnIdle()
    {
        if (isIdle) return;

        AllToFalse();

        isIdle = true;
        weaponAnimator.SetBool("isWalking", false);
        weaponAnimator.SetBool("isRunning", false);
        weaponAnimator.SetBool("isCrouchingWalk", false);
    }

    private void AllToFalse()
    {
        isIdle = false;
        isWalking = false;
        isRunning = false;
        isCrouching = false;
    }
}
