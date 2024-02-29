using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
