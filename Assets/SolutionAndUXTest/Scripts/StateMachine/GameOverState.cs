using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : State
{
    public override void Initialize()
    {
        base.Initialize();
        stateType = ImportantTypes.GameplayStates.GameOverState;
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