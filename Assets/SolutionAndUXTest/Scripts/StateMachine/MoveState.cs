using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    private void Start()
    {
        StateType = ImportantTypes.GameplayStates.MoveState;
    }
}
