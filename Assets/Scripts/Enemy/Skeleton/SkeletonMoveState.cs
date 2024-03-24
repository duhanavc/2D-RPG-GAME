using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundedState
{
    
    public SkeletonMoveState(EnemyStateMachine _stateMachine, Enemy _enemyBase, string _animBoolName, Enemy_Skeleton enemy) : base(_stateMachine, _enemyBase, _animBoolName, enemy)
    {

    }

    public override void Enter()
    {
        base.Enter();
        
    }


    public override void Update()
    {
        base.Update();

        enemy.setVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);

        if(enemy.isWallDedected() || !enemy.isGroundDedected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.skeletonIdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
