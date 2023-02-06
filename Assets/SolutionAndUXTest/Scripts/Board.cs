using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    public Vector2Int BoardSize;
    public ImportantTypes.TileType TyleType;
    public List<GameObject> TilePrefabs;
    private List<Tile> _tiles;
    private GameObject _parent;



    private void Awake()
    {
        _parent = GameObject.FindGameObjectWithTag("Board");
        _tiles = new List<Tile>();
        CreateBoardWithConfig(BoardSize, TyleType);
    }

    public void CreateBoardWithConfig(Vector2Int size, ImportantTypes.TileType tileType)
    {
        Vector3 _location = Vector3.zero;
        switch (tileType)
        {
            case ImportantTypes.TileType.Hexagon:
                GameObject selectedHexagonTile = VerifyTileType(ImportantTypes.TileType.Hexagon);

                for (int column = 0; column < size.x; column++)
                {
                    for (int row = 0; row < size.y * 3f; row++)
                    {
                        float width = Mathf.Sqrt(3);
                        if (row % 2 == 0)
                        {
                            _location = new Vector3((column * width) + width / 2f, 0, -row * 0.5f);
                        }

                        else
                        {
                            _location = new Vector3(column * width, 0, -row * 0.5f);
                        }

                        GameObject _instance = Instantiate(selectedHexagonTile, _location, quaternion.identity);
                        _instance.GetComponentInChildren<Tile>().Initialize();
                        _instance.name = (row+","+column);
                        _instance.transform.parent = _parent.transform;
                        _tiles.Add(_instance.GetComponentInChildren<Tile>());
                    }
                }

                break;

            case ImportantTypes.TileType.Square:

                GameObject selectedSquareTile = VerifyTileType(ImportantTypes.TileType.Square);

                for (int column = 0; column < size.x; column++)
                {
                    for (int row = 0; row < size.y; row++)
                    {
                        _location = new Vector3(column, 0, row);

                        GameObject _instance = Instantiate(selectedSquareTile, _location, quaternion.identity);
                        
                        _instance.GetComponent<Tile>().Initialize();
                        _instance.name = (row+","+column);
                        _instance.transform.parent = _parent.transform;
                        _tiles.Add(_instance.GetComponent<Tile>());
                    }
                }
                break;
        }
    }

    public GameObject VerifyTileType(ImportantTypes.TileType tileType)
    {
        foreach (var tile in TilePrefabs)
        {
            switch (tileType)
            {
                case ImportantTypes.TileType.Hexagon:
                    if (tile.GetComponentInChildren<Tile>().TileType == tileType)
                    {
                        return tile;
                    }
                    break;
                case ImportantTypes.TileType.Square:
                    if (tile.GetComponent<Tile>().TileType == tileType)
                    {
                        return tile;
                    }
                    break;
            }
        }
        return null;
    }

    public Tile GetRandomTile()
    {
        return _tiles[Random.Range(0, _tiles.Count)];
    }
}