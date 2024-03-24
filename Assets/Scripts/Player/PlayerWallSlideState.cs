using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {

        base.Update();
        if (Input.GetKeyDown(KeyCode.Space)) { 
            stateMachine.changeState(player.wallJumpState);
            return;
        }

        if(xInput != 0 && player.facingDir != xInput)
        {
            
            stateMachine.changeState(player.idleState);

        }


        if (yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);   
        else
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);

        if(player.isGroundDedected())
            stateMachine.changeState(player.idleState);

            
        if(!player.isWallDedected())
            stateMachine.changeState(player.idleState);
        

    }

}
