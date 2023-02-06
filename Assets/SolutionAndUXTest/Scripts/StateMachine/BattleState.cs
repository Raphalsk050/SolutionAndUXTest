using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : State
{
    public override void Initialize()
    {
        base.Initialize();
        stateType = ImportantTypes.GameplayStates.BattleState;
    }

    public override void PreEnterState()
    {
        base.PreEnterState();
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void PreExitingState()
    {
        base.PreExitingState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}