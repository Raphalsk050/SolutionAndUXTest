using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dice", fileName = "NewDice")]
public class Dice : ScriptableObject
{
    [Range(6, 20)] public int SideCount = 6;

    public int RollDice(int numberOfSides)
    {
        int face = Random.Range(1, numberOfSides);
        return face;
    }
}