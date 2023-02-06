using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
    private StateMachine _stateMachine;
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CameraTarget _cameraTarget;
    
    public Character[] Characters => _players;
    public Board Board => _board;
    public Character CurrentPlayer => _currentPlayer;

    private void Awake()
    {
        _turnManager = GetComponent<TurnManager>();

        _turnManager.TurnPassed += SwitchPlayer;
        
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        _cameraTarget = FindObjectOfType<CameraTarget>();

        
        
        _stateMachine = FindObjectOfType<StateMachine>();
        
        var charactersFound = FindObjectsOfType<Character>();

        _players = charactersFound;

        _board = FindObjectOfType<Board>();

        //warning players about tile kind to decide the direction to analyze
        foreach (Character player in _players)
        {
            player.boardTileType = _board.BoardConfig.TileType;
        }
    }

    private void Start()
    {
        _cameraTarget.MovementFinished += SetupPlayer;
        _currentPlayer = _players[0];
        TileClicked += MovePlayer;
        SwitchPlayer(0);

    }

    public void MovePlayer(GameObject Tile)
    {
        if (_currentPlayer.PlayerActionState != ImportantTypes.PlayerActionStates.Awaiting ||
            _stateMachine.CurrentState.StateType != ImportantTypes.GameplayStates.MoveState)
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
        
        
    }

    public void SwitchPlayer(int currentTurn)
    {
        _currentPlayer.MovementConcluded -= OnMovementConcluded;
        _currentPlayer.PlayerController.AllMovementsClear -= _turnManager.PassTurn;
        _currentPlayer.PlayerActionState = ImportantTypes.PlayerActionStates.Moving;

        _currentPlayer = _players[currentTurn % _players.Length];
        _cameraTarget.transform.SetParent(null);
        _cameraTarget.FollowPlayer(_currentPlayer.gameObject);
    }

    public void SetupPlayer()
    {
        _cameraTarget.transform.SetParent(_currentPlayer.transform);
        _currentPlayer.MovementConcluded += OnMovementConcluded;
        _currentPlayer.PlayerActionState = ImportantTypes.PlayerActionStates.Awaiting;
        _currentPlayer.PlayerController.AllMovementsClear += _turnManager.PassTurn;
        _currentPlayer.PlayerController.ResetPlayerMovement();
    }
}