using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;

    private float comboWindow = 2;

    public PlayerPrimaryAttackState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        xInput = 0;//attack direction ile ilgili bir bug'� d�zeltmek i�in buna ihtiyac�m�z var.


        if(comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow) 
            comboCounter = 0;
        Debug.Log(comboCounter);

        player.anim.SetInteger("ComboCounter", comboCounter );
        stateTimer = .1f;

        

        float attackDir = player.facingDir;

        if(xInput !=0){

            attackDir = xInput;
        
        }

        
        player.setVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .15f); // IEnumerator fonksiyonlar� al���la gelmi� fonksiyon �a�r�lar� �eklinde �a�r�lam�yor bu �ekilde �a�r�yoruz.

        comboCounter++;

        lastTimeAttacked = Time.time;

        
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            player.SetZeroVelocity();

        if(triggerCalled)
            player.stateMachine.changeState(player.idleState);


    }
}
