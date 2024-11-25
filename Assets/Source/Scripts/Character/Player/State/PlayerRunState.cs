
using UnityEngine;

public class PlayerRunState : IState
{
    private PlayerController playerCtrl;
    private PlayerStateMachine playerState;

    public PlayerRunState(PlayerController _playerCtrl, PlayerStateMachine playerState)
    {
        this.playerCtrl = _playerCtrl;
        this.playerState = playerState;
    }

    public void Enter()
    {
        playerCtrl.anim.SetBool("isRunning", true);
    }

    public void Excute()
    {
        float moveInput = InputManager.Instance.MoveInput();

        playerCtrl.playerDirectional.ChangeDirection(new Vector2(moveInput, 0));

        Vector2 moveVelocity = new Vector2(moveInput * playerCtrl.PlayerDataSO.moveSpeed, playerCtrl.rb.velocity.y);
        playerCtrl.rb.velocity = moveVelocity;

        if (playerState.TryJumpState()) return;
        if (playerState.TryWallSlideState()) return;
        if (playerState.TryIdleState()) return;
    }

    public void Exit()
    {
        playerCtrl.anim.SetBool("isRunning", false);
    }
}
