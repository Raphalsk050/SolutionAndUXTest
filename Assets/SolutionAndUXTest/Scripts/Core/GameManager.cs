using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void OnTileClicked(GameObject Tile);

    public OnTileClicked TileClicked;
    private Character[] _players;
    private Character _currentPlayer;
    private TurnManager _turnManager;
    private GameObject _currentTileLocation;
    private Board _board;
    private GameObject _lastTileLocation;

    public Character[] Characters => _players;
    public Board Board => _board;
    
    private void Awake()
    {
        var charactersFound = FindObjectsOfType<Character>();

        _players = charactersFound;

        _board = FindObjectOfType<Board>();

        //warning players about tile kind to decide the direction to analyze
        foreach (Character player in _players)
        {
            player.boardTileType = _board.TyleType;
        }
    }

    private void Start()
    {
        _currentPlayer = _players[0];
        _currentPlayer.MovementConcluded += OnMovementConcluded;
        TileClicked += MovePlayer;
    }

    public void MovePlayer(GameObject Tile)
    {
        if (_currentPlayer.ActionState != ImportantTypes.PlayerActionStates.Awaiting)
        {
            return;
        }

        if (_currentPlayer.MoveToTile(Tile, false))
        {
            _lastTileLocation = _currentTileLocation;
            _currentTileLocation = Tile;
        }
    }

    private void OnMovementConcluded()
    {
        //when the player is able to move again
    }
}