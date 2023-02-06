using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupState : State
{
    private Character[] _characters;
    private Tile _firstTileSelected;
    private Tile _secondTileSelected;
    private bool _tileFound;
    private Board _board;
    private GameManager _gameManager;

    public override void Initialize()
    {
        base.Initialize();
        stateType = ImportantTypes.GameplayStates.SetupState;
        nextState = ImportantTypes.GameplayStates.MoveState;
        _board = FindObjectOfType<Board>();
        _gameManager = FindObjectOfType<GameManager>();
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
        EnterState();
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

        foreach (var character in _characters)
        {
            foreach (var tile in character.NearTiles)
            {
                tile.SetCollectable(tile.collectable[0]);
                _gameManager.CurrentPlayer.CurrentTile.SetCollectable(tile.collectable[0]);
            }
        }
        _characters[0].MovementConcluded -= FirstCharacterMovementConcluded;
        _characters[1].MovementConcluded -= SecondCharacterMovementConcluded;
        PreExitingState();
    }

    public override void PreExitingState()
    {
        ExitState();
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