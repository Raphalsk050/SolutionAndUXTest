using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Vector2Int BoardSize;
    public ImportantTypes.TileType TyleType;
    public List<GameObject> TilePrefabs;
    private GameObject _parent;


//this is used only for debug proposes. Used to preview board instanced tiles.
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
    }
#endif

    private void Start()
    {
        _parent = GameObject.FindGameObjectWithTag("Board");
        CreateBoardWithConfig(BoardSize, TyleType);
    }

    public void CreateBoardWithConfig(Vector2Int size, ImportantTypes.TileType tileType)
    {
        Vector3 _location = Vector3.zero;
        int _height = 0;
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
                        _instance.GetComponent<Tile>().Initialize();
                        _instance.transform.parent = _parent.transform;
                    }
                }

                break;

            case ImportantTypes.TileType.Square:

                GameObject selectedSquareTile = VerifyTileType(ImportantTypes.TileType.Square);

                for (int i = 0; i < size.x; i++)
                {
                    for (int j = 0; j < size.y; j++)
                    {
                        _location = new Vector3(i, 0, j);

                        GameObject _instance = Instantiate(selectedSquareTile, _location, quaternion.identity);
                        _instance.GetComponent<Tile>().Initialize();
                        _instance.transform.parent = _parent.transform;
                        
                    }
                }
                break;
        }
    }

    public GameObject VerifyTileType(ImportantTypes.TileType tileType)
    {
        foreach (var tile in TilePrefabs)
        {
            if (tile.GetComponent<Tile>().TileType == tileType)
            {
                return tile;
            }
        }

        return null;
    }
}