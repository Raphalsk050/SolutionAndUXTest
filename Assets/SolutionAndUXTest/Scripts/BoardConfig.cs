using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBoardConfig", menuName = "BoardConfig")]
public class BoardConfig : ScriptableObject
{
    public Vector2Int BoardSize = new Vector2Int(16,16);
    public ImportantTypes.TileType TileType = ImportantTypes.TileType.Hexagon;
}
