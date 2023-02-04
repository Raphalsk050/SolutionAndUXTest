using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Collectable", fileName = "NewCollectable")]
public class Collectable : ScriptableObject
{
    public Material Material;
    private int _value;
    
    public int Value
    {
        get => _value;
        set => _value = value;
    }
    
}
