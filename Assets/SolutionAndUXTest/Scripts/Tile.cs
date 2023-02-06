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
        Collectable selectedCollectable = collectable[Random.Range(0, collectable.Count)];
        if (GetComponent<MeshRenderer>())
        {
            _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
            GetComponent<MeshRenderer>().sharedMaterial = selectedCollectable.Material;
            return;
        }

        transform.GetComponentInChildren<MeshRenderer>().sharedMaterial = selectedCollectable.Material;
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        
    }

    void OnMouseDown()
    {
        _gameManager.TileClicked(gameObject);
    }
}