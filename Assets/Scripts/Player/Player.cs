using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Attack Details")]
    public Vector2[] attackMovement;
    


    [Header("Move ›nfo")]
    public float moveSpeed = 8f;
    public float jumpForce;

    [Header("Dash Info")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDirection { get; private set; }

    public bool isBusy {  get; private set; } 


    #region States
    public PlayerStateMachine stateMachine { get; private set; }// means this value is read-only to other scripts atleast.
    public PlayerIdleState idleState {  get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }  
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }    
    #endregion

    protected override void Awake()
    {

        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
        jumpState = new PlayerJumpState(stateMachine, this, "Jump");
        airState = new PlayerAirState(stateMachine, this, "Jump");
        dashState = new PlayerDashState(stateMachine, this, "Dash");
        wallSlideState = new PlayerWallSlideState(stateMachine, this, "WallSlide");
        wallJumpState = new PlayerWallJumpState(stateMachine, this, "Jump");
        primaryAttackState = new PlayerPrimaryAttackState(stateMachine, this, "Attack");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.initialize(idleState);
    }


    protected override void Update()
    {       
        base.Update(); 

        stateMachine.currentState.Update();
        CheckForDash›nput();             
    }

    public IEnumerator BusyFor(float _seconds) // coroutine bize istediimiz zaman pauselar yapmam˝z˝ sal˝yor
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds); // bi ara bu new muhabbetini sor.
        isBusy = false;

    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    private void CheckForDash›nput()
    {
        
        if(isWallDedected())
            return;

        dashUsageTimer -= Time.deltaTime;

        if (dashUsageTimer < 0 && Input.GetKeyDown(KeyCode.LeftShift))
        {

            dashUsageTimer = dashCooldown;
            dashDirection = Input.GetAxisRaw("Horizontal");

            if (dashDirection == 0)
                dashDirection = facingDir;

            stateMachine.changeState(dashState);

        }
    }

    

  
} 
