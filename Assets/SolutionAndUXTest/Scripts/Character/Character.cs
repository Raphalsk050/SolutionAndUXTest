using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LifeComponent), typeof(AbiltyComponent), typeof(PlayerController))]
public class Character : MonoBehaviour
{
    public delegate void OnMovementCompleted();

    public event OnMovementCompleted MovementConcluded;
    public AnimationCurve movementCurve;
    public Dice[] PlayerDices;
    public ImportantTypes.TileType boardTileType;

    //i know, this is dynamic allocated, i'm out of time :)
    private List<Tile> _nearTiles;
    private Tile _currentTile;
    private ImportantTypes.PlayerActionStates _playerActionState;

    public ImportantTypes.PlayerActionStates PlayerActionState
    {
        get => _playerActionState;
        set => _playerActionState = value;
    }

    private PlayerController _playerController;

    //i know, this is dynamic allocated, i'm out of time :)
    private List<Vector3> _checkDirections;


    public List<Tile> NearTiles => _nearTiles;
    public Tile CurrentTile => _currentTile;
    public PlayerController PlayerController => _playerController;


    private void Start()
    {
        _checkDirections = new List<Vector3>();
        _nearTiles = new List<Tile>();
        _playerController = GetComponent<PlayerController>();
        _playerActionState = ImportantTypes.PlayerActionStates.Moving;

        switch (boardTileType)
        {
            case ImportantTypes.TileType.Hexagon:
                _checkDirections.Add(new Vector3(1, -1, 1));
                _checkDirections.Add(new Vector3(0, -1, 1));
                _checkDirections.Add(new Vector3(-1, -1, 1));
                _checkDirections.Add(new Vector3(-1, -1, -1));
                _checkDirections.Add(new Vector3(0, -1, -1));
                _checkDirections.Add(new Vector3(1, -1, -1));

                break;
            case ImportantTypes.TileType.Square:
                _checkDirections.Add(transform.forward - transform.up);
                _checkDirections.Add(-transform.forward - transform.up);
                _checkDirections.Add(transform.right - transform.up);
                _checkDirections.Add(-transform.right - transform.up);
                break;
        }
    }

    public bool MoveToTile(GameObject tile, bool initialMove)
    {
        if (initialMove)
        {
            StartCoroutine(ValidateCurve(0.5f, tile));

            return true;
        }

        foreach (Tile nearTile in _nearTiles)
        {
            if (tile.GetComponent<Tile>() == nearTile)
            {
                StartCoroutine(ValidateCurve(0.5f, tile));
                _playerController.UsePlayerMovement();
                return true;
            }
        }

        return false;
    }

    private IEnumerator ValidateCurve(float duration, GameObject tile)
    {
        float initialTime = Time.time;
        float endTime = initialTime + duration;
        float t;
        Vector3 currentPosition = transform.position;

        _playerActionState = ImportantTypes.PlayerActionStates.Moving;

        while (Time.time < endTime)
        {
            t = Mathf.InverseLerp(initialTime, endTime, Time.time);
            Vector3 newPosition = tile.transform.position + Vector3.up * 0.394f;
            transform.position = Vector3.Lerp(currentPosition, newPosition, movementCurve.Evaluate(t));
            yield return null;
        }

        _playerActionState = ImportantTypes.PlayerActionStates.Awaiting;
        VerifyNearTiles(tile);
    }

    public void VerifyNearTiles(GameObject currentTile)
    {
        RaycastHit raycastHit;

        _nearTiles.Clear();
        foreach (Vector3 direction in _checkDirections)
        {
            if (Physics.Raycast(transform.position + Vector3.up / 2, direction, out raycastHit))
            {
                if (raycastHit.transform.gameObject.GetComponent<Tile>())
                {
                    _nearTiles.Add(raycastHit.transform.gameObject.GetComponent<Tile>());
                }
                else
                {
                    _nearTiles.Add(raycastHit.transform.gameObject.GetComponentInChildren<Tile>());
                }
            }
        }

        _currentTile = currentTile.GetComponent<Tile>();

        MovementConcluded();
    }
}