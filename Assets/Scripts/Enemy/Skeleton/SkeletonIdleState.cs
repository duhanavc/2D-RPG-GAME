using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundedState
{
    public SkeletonIdleState(EnemyStateMachine _stateMachine, Enemy _enemyBase, string _animBoolName, Enemy_Skeleton enemy) : base(_stateMachine, _enemyBase, _animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
        
       
    }


    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.skeletonMoveState);
    }
    public override void Exit()
    {
        base.Exit();
    }

}
