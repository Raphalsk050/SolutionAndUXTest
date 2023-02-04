using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LifeComponent : Component
{
    public delegate void OnTakeDamage();
    public OnTakeDamage TookDamage;
    public int MaxLife;
    private bool _alive = true;
    private int _currentLife;
    
    private void Start()
    {
        _currentLife = MaxLife;
    }

    public void TakeDamage(int damageAmount)
    {
        if (_alive)
        {
            int comparedDamage = _currentLife - damageAmount;
            _currentLife =  comparedDamage > 0 ? comparedDamage : 0;
            _alive = comparedDamage > 0;
            TookDamage();
        }
    }
}
