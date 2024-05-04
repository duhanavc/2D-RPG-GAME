using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
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

        if(Input.GetKeyDown(KeyCode.Mouse1))
            stateMachine.changeState(player.aimSwordState);

        if(Input.GetKeyDown(KeyCode.Q))
            stateMachine.changeState(player.counterAttackState);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.changeState(player.primaryAttackState);
                
        if(!player.isGroundDedected())
        {
            stateMachine.changeState(player.airState);
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && player.isGroundDedected())
        {
            stateMachine.changeState(player.jumpState);
        }
        
    }
}
