using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : State
{
    private void Start()
    {
        StateType = ImportantTypes.GameplayStates.GameOverState;
    }
}
