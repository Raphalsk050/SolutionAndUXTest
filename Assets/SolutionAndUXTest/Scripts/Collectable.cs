using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Collectable", fileName = "NewCollectable")]
public class Collectable : ScriptableObject
{
    public Material Material;
    public float ChanceToSpawn = 0;
    private float _value = 1f;

    public float Value => _value;
}