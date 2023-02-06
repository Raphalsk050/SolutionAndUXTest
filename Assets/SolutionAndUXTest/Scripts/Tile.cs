using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    public ImportantTypes.TileType TileType;
    
    public delegate void StateChange(ImportantTypes.TileStates newState);

    public StateChange OnTileStateChanged;
    public List<Collectable> collectable;
    public Sprite TileSprite;
    public Vector2Int tilePositionMatrix;
    private ImportantTypes.TileStates _tileState;
    private GameManager _gameManager;
    private Collider _tileCollider;

    public void ChangeToTileState(ImportantTypes.TileStates state)
    {
        _tileState = state;
        OnTileStateChanged(_tileState);
    }

    public void Initialize()
    {
        var col = collectable[Random.Range(0, collectable.Count)];
        if (Random.Range(0f,1f) < col.ChanceToSpawn)
        {
            SetCollectable(col);
        }
        else
        {
            SetCollectable(collectable[0]);
        }
    }

    void OnMouseDown()
    {
        _gameManager.TileClicked(gameObject);
    }

    public void SetCollectable(Collectable newCollectable)
    {
        if (GetComponent<MeshRenderer>())
        {
            _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
            GetComponent<MeshRenderer>().sharedMaterial = newCollectable.Material;
            return;
        }

        transform.GetComponentInChildren<MeshRenderer>().sharedMaterial = newCollectable.Material;
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }
}