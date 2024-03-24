using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {


    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = .4f;
        player.setVelocity(-player.facingDir * 5, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0) {
            stateMachine.changeState(player.airState);   
        }

        if(player.isGroundDedected())
            stateMachine.changeState(player.idleState);
        


    }
}
