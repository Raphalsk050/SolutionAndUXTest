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
    private ImportantTypes.TileStates _tileState;
    

    public void ChangeToTileState(ImportantTypes.TileStates state)
    {
        _tileState = state;
        OnTileStateChanged(_tileState);
    }

    public void Initialize()
    {
        var selectedCollectable = collectable[Random.Range(0, collectable.Count)];
        GetComponent<MeshRenderer>().sharedMaterial = selectedCollectable.Material;
        
    }
}
