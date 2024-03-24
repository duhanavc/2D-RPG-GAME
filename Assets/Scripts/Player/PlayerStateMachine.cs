using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState{ get; private set; } //get default olarak public set ise private tanýmladýk bu þu demek
                                                         //value is public if you want to get it , but if you want to change it it is private.

    public void initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void changeState(PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
