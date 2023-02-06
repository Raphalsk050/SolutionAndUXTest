using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    #region Delegates

    public delegate void OnStateChanged();

    #endregion

    #region publicVariables

    public OnStateChanged OnEnteringState;
    public OnStateChanged OnEnteredState;
    public OnStateChanged OnExitingState;
    public OnStateChanged OnExitedState;

    #endregion

    #region ProtectedVariables

    protected ImportantTypes.GameplayStates stateType;
    protected ImportantTypes.GameplayStates nextState;

    protected StateMachine StateMachine;
    protected GameManager GameManager;

    #endregion


    public ImportantTypes.GameplayStates StateType => stateType;

    public virtual void Initialize()
    {
        StateMachine = GetComponent<StateMachine>();
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    public virtual void PreEnterState()
    {
        
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
        StateMachine.ChangeToGameplayState(nextState);
        OnExitedState();
    }
}