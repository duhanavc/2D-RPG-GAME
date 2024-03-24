using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState
{

    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;


    protected bool triggerCalled;

    protected float xInput;
    protected float yInput;


    protected float stateTimer;

    private string animBoolName;


    public PlayerState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName)
    {
        this.stateMachine = _stateMachine;
        this.player = _player;
        this.animBoolName = _animBoolName;
    }

    
    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);

    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        yInput = Input.GetAxisRaw("Vertical");
        xInput = Input.GetAxisRaw("Horizontal");
        player.anim.SetFloat("yVelocity",rb.velocity.y);

    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }


}
