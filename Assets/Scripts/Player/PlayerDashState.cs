using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
        _gravityScale = rb.gravityScale;
        rb.gravityScale = 0;

        player.stats.MakeInvincible(true); // 无敌开启
        player.skill.dash.CloneOnDash();
    }

    public override void Exit()
    {
        base.Exit();

        rb.gravityScale = _gravityScale;
        player.SetVelocity(0, rb.velocity.y);

        player.stats.MakeInvincible(false); // 无敌关闭
        player.skill.dash.CloneOnArrival();
    }

    public override void Update()
    {
        base.Update();
       
        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);

        player.SetVelocity(player.dashSpeed * player.dashDir, 0);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);

        player.fx.CreateAfterImage();
    }
}
