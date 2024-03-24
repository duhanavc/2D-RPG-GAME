using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Enemy_Skeleton enemy;
    private Transform player;
    private int moveDir;

    public SkeletonBattleState(EnemyStateMachine _stateMachine, Enemy _enemyBase, string _animBoolName, Enemy_Skeleton _enemy) : base(_stateMachine, _enemyBase, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = GameObject.Find("Player").transform; 
        
    }
    public override void Update()
    {
        base.Update();
        

        if (enemy.isPlayerDedected())
        {
            stateTimer = enemy.battleTime;
            if (enemy.isPlayerDedected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(enemy.skeletonAttackState);
            }
        }
        else
        {

            if (stateTimer < 0 || Vector2.Distance(player.position, enemy.transform.position) > 10)
            {
                stateMachine.ChangeState(enemy.skeletonIdleState);
            }
        
        }

          

        if(player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if(player.position.x < enemy.transform.position.x)
            moveDir = -1;   
        
        enemy.setVelocity(moveDir * enemy.moveSpeed, rb.velocity.y);


    }

    public override void Exit()
    {
        base.Exit();
        
    }

    private bool CanAttack()
    {
        if(Time.time >= enemy.attackCooldown + enemy.lastTimeAttacked)
        {
           enemy.lastTimeAttacked = Time.time;
           return true;
        }
        
        return false;
    }
}
