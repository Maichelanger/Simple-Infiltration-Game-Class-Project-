using System;
using UnityEngine;

public class PlayerAnimationsController : MonoBehaviour
{
    public Animator weaponAnimator;
    public Animator fpCameraAnimator;

    private PlayerData playerData;

    private void Awake()
    {
        playerData = GetComponent<PlayerData>();
    }

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
            case "aim":
                OnAim();
                break;
        }
    }

    private void OnWalk()
    {
        if (playerData.isWalking) return;
        else if (playerData.isCrouching && playerData.isIdle)
        {
            OnCrouchWalk();
            return;
        }

        if (playerData.isAiming) fpCameraAnimator.SetBool("isAiming", true);


        AllToFalse();

        playerData.isWalking = true;
        weaponAnimator.SetBool("isWalking", true);
        weaponAnimator.SetBool("isRunning", false);
        weaponAnimator.SetBool("isCrouchingWalk", false);
        weaponAnimator.SetBool("isAiming", false);
    }

    private void OnSprint()
    {
        if (playerData.isRunning) return;
        if (playerData.isAiming) fpCameraAnimator.SetBool("isAiming", false);

        AllToFalse();

        playerData.isRunning = true;
        weaponAnimator.SetBool("isWalking", false);
        weaponAnimator.SetBool("isRunning", true);
        weaponAnimator.SetBool("isCrouchingWalk", false);
        weaponAnimator.SetBool("isAiming", false);
    }

    private void OnCrouchWalk()
    {
        if (playerData.isCrouchingWalk) return;
        if (playerData.isAiming) fpCameraAnimator.SetBool("isAiming", true);

        AllToFalse();

        playerData.isCrouchingWalk = true;
        weaponAnimator.SetBool("isWalking", false);
        weaponAnimator.SetBool("isRunning", false);
        weaponAnimator.SetBool("isCrouchingWalk", true);
        weaponAnimator.SetBool("isAiming", false);
    }

    private void OnIdle()
    {
        if (playerData.isIdle) return;
        else if (playerData.isAiming)
        {
            OnAim();
            return;
        }

        AllToFalse();

        playerData.isIdle = true;
        weaponAnimator.SetBool("isWalking", false);
        weaponAnimator.SetBool("isRunning", false);
        weaponAnimator.SetBool("isCrouchingWalk", false);
        weaponAnimator.SetBool("isAiming", false);
    }

    private void OnAim()
    {
        if (playerData.isAiming) return;

        playerData.isAiming = true;

        fpCameraAnimator.SetBool("isAiming", true);
        weaponAnimator.SetBool("isAiming", true);
    }

    public void DisableAim()
    {
        if (!playerData.isAiming) return;

        playerData.isAiming = false;
        fpCameraAnimator.SetBool("isAiming", false);
        weaponAnimator.SetBool("isAiming", false);

        UpdateStates("idle");
    }

    private void AllToFalse()
    {
        playerData.isIdle = false;
        playerData.isWalking = false;
        playerData.isRunning = false;
        playerData.isCrouchingWalk = false;
    }

    internal string GetCurrentState()
    {
        if (playerData.isIdle) return "idle";
        if (playerData.isWalking) return "walk";
        if (playerData.isRunning) return "run";
        if (playerData.isCrouching) return "crouch";

        return "idle";
    }
}
