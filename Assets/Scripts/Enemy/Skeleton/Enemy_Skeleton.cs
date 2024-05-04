using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{
   
    #region STATES

    public SkeletonIdleState skeletonIdleState {  get; private set; }
    public SkeletonMoveState skeletonMoveState {  get; private set; }
    public SkeletonBattleState skeletonBattleState {  get; private set; }
    public SkeletonAttackState skeletonAttackState {  get; private set; }
    public SkeletonStunnedState skeletonStunnedState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        skeletonIdleState = new SkeletonIdleState(stateMachine, this, "Idle", this);
        skeletonMoveState = new SkeletonMoveState(stateMachine, this, "Move", this);
        skeletonBattleState = new SkeletonBattleState(stateMachine, this, "Move", this);
        skeletonAttackState = new SkeletonAttackState(stateMachine, this, "Attack", this);
        skeletonStunnedState = new SkeletonStunnedState(stateMachine, this, "Stunned", this);

    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(skeletonIdleState);
    }

    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.U))
        {
            stateMachine.ChangeState(skeletonStunnedState);
        }
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(skeletonStunnedState);
            return true;
        }
        return false;
    
    }




     



}
