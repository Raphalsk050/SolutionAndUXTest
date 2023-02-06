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

    //i know this is dynamic allocated, i'm out of time :)
    private List<Tile> _nearTiles;
    private Tile _currentTile;
    private ImportantTypes.PlayerActionStates _playerActionState;

    //i know this is dynamic allocated, i'm out of time :)
    private List<Vector3> _checkDirections;


    public ImportantTypes.PlayerActionStates ActionState => _playerActionState;
    public List<Tile> NearTiles => _nearTiles;
    public Tile CurrentTile => _currentTile;
    

    private void Start()
    {
        _checkDirections = new List<Vector3>();
        _nearTiles = new List<Tile>();

        _playerActionState = ImportantTypes.PlayerActionStates.Awaiting;

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
                _checkDirections.Add(new Vector3(1, -1, 0));
                _checkDirections.Add(new Vector3(0, -1, 1));
                _checkDirections.Add(new Vector3(-1, -1, 1));
                _checkDirections.Add(new Vector3(0, -1, -1));
                break;
        }
    }

    public bool MoveToTile(GameObject tile, bool initialMove)
    {
        if (initialMove)
        {
            StartCoroutine(ValidateCurve(0.5f, tile.transform.position));
            _currentTile = tile.GetComponent<Tile>();
            return true;
        }

        foreach (Tile nearTile in _nearTiles)
        {
            if (tile.GetComponent<Tile>() == nearTile)
            {
                StartCoroutine(ValidateCurve(0.5f, tile.transform.position));
                return true;
            }
        }
        
        return false;
    }

    private IEnumerator ValidateCurve(float duration, Vector3 tilePosition)
    {
        float initialTime = Time.time;
        float endTime = initialTime + duration;
        float t;
        Vector3 currentPosition = transform.position;

        _playerActionState = ImportantTypes.PlayerActionStates.Moving;

        while (Time.time < endTime)
        {
            t = Mathf.InverseLerp(initialTime, endTime, Time.time);
            Vector3 newPosition = tilePosition + Vector3.up * 0.394f;
            transform.position = Vector3.Lerp(currentPosition, newPosition, movementCurve.Evaluate(t));
            yield return null;
        }
        
        _playerActionState = ImportantTypes.PlayerActionStates.Awaiting;
        VerifyNearTiles();
    }

    public void VerifyNearTiles()
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
                    raycastHit.transform.gameObject.GetComponentInChildren<Tile>();
                }
            }
        }
        MovementConcluded();
    }
}