using UnityEngine;

public enum PlayerStateId
{
    Idle,
    Walking,
    Running,
    Crouching,
    CrouchingWalk,
    Aiming,
    WalkingAiming,
}

public interface PlayerState
{
    PlayerStateId GetStateId();

    void Enter(PlayerAgent agent);
    void Update(PlayerAgent agent);
    void Exit(PlayerAgent agent);

}
