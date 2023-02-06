using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public delegate void OnTurnPassed(int currentTurn);

    public OnTurnPassed TurnPassed;
    private int _currentTurn;
    public int CurrentTurn => _currentTurn;
    
    public void PassTurn()
    {
        _currentTurn += 1;
        Debug.Log("TurnPassed");
        TurnPassed(_currentTurn);
    }

}
