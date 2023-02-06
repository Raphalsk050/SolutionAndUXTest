using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private int _currentTurn;
    public int CurrenTurn => _currentTurn;
    
    public void PassTurn()
    {
        _currentTurn += 1;
    }

    public void StartTurn(Character player)
    {
        
    }
}
