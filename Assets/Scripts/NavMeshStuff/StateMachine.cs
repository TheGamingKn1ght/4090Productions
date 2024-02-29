using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState currentState;
    
    public void changeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void ExecuteState()
    {
        currentState.Execute();
    }
}
