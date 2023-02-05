using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public delegate void OnStateChanged();
    
    public OnStateChanged OnEnteringState;
    public OnStateChanged OnEnteredState;
    public OnStateChanged OnExitingState;
    public OnStateChanged OnExitedState;

    protected ImportantTypes.GameplayStates StateType;
    
    public virtual void PreEnterState()
    {
        OnEnteringState();
    }
    
    public virtual void EnterState()
    {
        OnEnteredState();
    }
    
    public virtual void PreExitingState()
    {
        OnExitingState();
    }
    
    public virtual void ExitState()
    {
        OnExitedState();
    }
    
}
