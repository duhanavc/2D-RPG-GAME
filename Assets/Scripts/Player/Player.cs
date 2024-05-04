using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Attack Details")]
    public Vector2[] attackMovement;
    public float playerCounterAttackDuration = .2f;


    [Header("Move Ýnfo")]
    public float moveSpeed = 8f;
    public float jumpForce;

    [Header("Dash Info")]
    //[SerializeField] private float dashCooldown;        skill managerle birlikte bu deðiþkenlere ihtiyacýmýz kalmadý
    //private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDirection { get; private set; }

    public bool isBusy {  get; private set; } 


    public SkillManager skill {  get; private set; }

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
    public PlayerCounterAttackState counterAttackState { get; private set; }
    public PlayerAimSwordState aimSwordState { get; private set; }
    public PlayerCatchSwordState catchSwordState { get; private set; }
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
        counterAttackState = new PlayerCounterAttackState(stateMachine, this, "CounterAttack");
        aimSwordState = new PlayerAimSwordState(stateMachine, this, "AimSword");
        catchSwordState = new PlayerCatchSwordState(stateMachine, this, "CatchSword");
    }

    protected override void Start()
    {
        base.Start();
        skill = SkillManager.instance; // herseferþnde skillman.instance yazmamak için yaptýk.
        stateMachine.initialize(idleState);
    }


    protected override void Update()
    {       
        base.Update(); 

        stateMachine.currentState.Update();
        CheckForDashÝnput();             
    }

    public IEnumerator BusyFor(float _seconds) // coroutine bize istediðimiz zaman pauselar yapmamýzý saðlýyor
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds); // bi ara bu new muhabbetini sor.
        isBusy = false;

    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    private void CheckForDashÝnput()
    {
        
        if(isWallDedected())
            return;


        //dashUsageTimer -= Time.deltaTime; artýk skill managerimiz olduðu için bunu silebiliriz ve kotnrolü can use skill fonksiyonuyla yapabiliriz.

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
        {

            //dashUsageTimer = dashCooldown; bunuda silebiliriz çünkü singleton olan skillmanager sayesinde can use skill fonskiyonuna eriþebiliyoruz.oda bu iþi yapýyor.
            dashDirection = Input.GetAxisRaw("Horizontal");

            if (dashDirection == 0)
                dashDirection = facingDir;

            stateMachine.changeState(dashState);

        }
    }
} 
