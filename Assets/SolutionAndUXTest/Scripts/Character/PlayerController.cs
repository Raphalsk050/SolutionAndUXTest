using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public delegate void OnMovementsClear();

    public OnMovementsClear AllMovementsClear;
    
    private int _playerMovement = 3;
    private Character _ownedPlayer;
    public int PlayerMovement => _playerMovement;

    private void Start()
    {
        _ownedPlayer = GetComponent<Character>();
        _ownedPlayer.MovementConcluded += CheckPlayerMovements;
    }

    public void UsePlayerMovement()
    {
        if (_playerMovement > 0)
        {
            _playerMovement--;
            
        }
    }

    public void ResetPlayerMovement()
    {
        _playerMovement = 3;
    }

    public void CheckPlayerMovements()
    {
        if (_playerMovement == 0)
        {
            AllMovementsClear();
        }
    }
    
}
