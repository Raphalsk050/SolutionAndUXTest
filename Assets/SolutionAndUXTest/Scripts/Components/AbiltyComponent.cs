using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbiltyComponent : MonoBehaviour
{
    public delegate void OnReceivedAbility();

    public delegate int OnMoveCountChanged();

    public OnReceivedAbility MovementReceived;
    public OnMoveCountChanged MoveAmountChanged;
    public OnReceivedAbility DamageMultiplierReceived;

    private int _moveCount = 3;
    private float _damageMultiplier = 1f;
    private float _defaultDamageMultiplier = 1f;

    public int MoveCount
    {
        get => _moveCount;
    }

    public float DamageMultiplier
    {
        get => _damageMultiplier;
    }


    public void AddMovementForThisTurn(int movementCount)
    {
        if (movementCount > 0)
        {
            _moveCount += movementCount;
            MovementReceived();
            MoveAmountChanged();
        }
    }

    public void AddDamageMultiplier(float amount)
    {
        if (amount > 1)
        {
            _damageMultiplier = amount;
            DamageMultiplierReceived();
            return;
        }

        _damageMultiplier = 1f;
    }

    public void UseMovement(int cost)
    {
        if (_moveCount - cost > 0)
        {
            _moveCount -= cost;
            MoveAmountChanged();
        }
    }

    public void ResetDamageMultiplier()
    {
        _damageMultiplier = _defaultDamageMultiplier;
    }
}