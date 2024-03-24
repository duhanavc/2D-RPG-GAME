using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    [SerializeField]protected LayerMask whatIsPlayer;

    [Header("Stunned Info")]
    public float stunDuration;
    public Vector2 stunDir;

    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;

    [Header("Attack Info")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector]public float lastTimeAttacked;

    public EnemyStateMachine stateMachine { get; private set; } // iki ayn� isimde declare var playerinki ile nas�l kar��m�yor ikiside publicken.

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

    }
    
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public virtual RaycastHit2D isPlayerDedected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir,transform.position.y));

    }
}
