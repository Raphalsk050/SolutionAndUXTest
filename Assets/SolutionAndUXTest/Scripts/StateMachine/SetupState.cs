using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupState : State
{
    private Character[] _characters;
    private Tile _firstTileSelected;
    private Tile _secondTileSelected;
    private bool _tileFound;

    public override void Initialize()
    {
        base.Initialize();
        stateType = ImportantTypes.GameplayStates.SetupState;
        nextState = ImportantTypes.GameplayStates.MoveState;
    }

    private void FirstCharacterMovementConcluded()
    {
        while (!_tileFound)
        {
            //first tile verified to not setup second player position orthogonally near to other player
            VerifyTile(_firstTileSelected);
        }
        
        _characters[1].MoveToTile(_secondTileSelected.gameObject, true);
        
    }

    private void SecondCharacterMovementConcluded()
    {
        Debug.Log("Setup board!");
    }

    public override void PreEnterState()
    {
        _characters = StateMachine.Characters;

        
        _characters[0].MovementConcluded += FirstCharacterMovementConcluded;
        _characters[1].MovementConcluded += SecondCharacterMovementConcluded;

        _firstTileSelected = GameManager.Board.GetRandomTile();
        _characters[0].MoveToTile(_firstTileSelected.gameObject, true);
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

    /**
     * verifies if the selected tile is able to receive the second player.
     * This used to not enter in the combat state or move the second player to first player location when game beginning.
     */
    
    private bool VerifyTile(Tile tile)
    {
        _secondTileSelected = GameManager.Board.GetRandomTile();
        foreach (var currentAnalisedTile in _characters[0].NearTiles)
        {
            if (currentAnalisedTile == _secondTileSelected || _characters[0].CurrentTile == _secondTileSelected)
            {
                return false;
            }
        }

        return _tileFound = true;
    }
}