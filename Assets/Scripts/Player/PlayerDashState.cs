using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {

    }

    public override void Enter()
    {
      
        base.Enter();
        player.skill.clone.CreateClone(player.transform);
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.setVelocity(0, rb.velocity.y);

    }

    public override void Update()
    {
        base.Update();
        
        if(!player.isGroundDedected() && player.isWallDedected())
            stateMachine.changeState(player.wallSlideState);

        player.setVelocity(player.dashSpeed * player.dashDirection, 0);

        if (stateTimer < 0)
            stateMachine.changeState(player.idleState);
    }
}
