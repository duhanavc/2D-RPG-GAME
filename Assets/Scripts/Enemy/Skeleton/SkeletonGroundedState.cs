using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundedState : EnemyState
{
    protected Enemy_Skeleton enemy;
    protected Transform player;
    public SkeletonGroundedState(EnemyStateMachine _stateMachine, Enemy _enemyBase, string _animBoolName, Enemy_Skeleton _enemy) : base(_stateMachine, _enemyBase, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;//GameObject.Find("Player").transform;
    }
    public override void Update()
    {
        base.Update();
        if (enemy.isPlayerDedected() || Vector2.Distance(enemy.transform.position, player.transform.position) < 2 )
            stateMachine.ChangeState(enemy.skeletonBattleState);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
