using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantTypes
{
    public enum TileStates
    {
        Full,
        Empty
    }

    public enum TileType
    {
        Hexagon,
        Square
    }

    public enum GameplayStates
    {
        MoveState,
        BattleState,
        SetupState,
        GameOverState
    }

    public enum PlayerActionStates
    {
        Moving,
        Awaiting,
        Attacking
    }
    
}
