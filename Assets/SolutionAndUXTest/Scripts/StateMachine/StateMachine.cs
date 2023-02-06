using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    #region PrivateVariables

    private State[] _states;
    private State _currentState;
    private Character[] _players;
    public Character[] Characters => _players;

    #endregion

    #region MonoMethods

    private void Awake()
    {
        _states = GetComponents<State>();
        _players = FindObjectsOfType<Character>();
        InitializeStates();
    }

    private void Start()
    {
        
        ChangeToGameplayState(ImportantTypes.GameplayStates.SetupState);
        
        
    }

    #endregion


    protected virtual void InitializeStates()
    {
        foreach (var state in _states)
        {
            state.Initialize();
            
        }
    }

    public void ChangeToGameplayState(ImportantTypes.GameplayStates newState)
    {
        foreach (var state in _states)
        {
            if (state.StateType == newState)
            {
                _currentState = state;
                state.PreEnterState();
                break;
            }
        }
    }
}