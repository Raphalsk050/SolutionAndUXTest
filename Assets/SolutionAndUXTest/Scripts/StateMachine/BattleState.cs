using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : State
{
    private void Start()
    {
        StateType = ImportantTypes.GameplayStates.BattleState;
    }

    public override void PreEnterState()
    {
        
    }
}
