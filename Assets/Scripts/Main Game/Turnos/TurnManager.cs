using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TurnManager : MonoBehaviour
{

    public int turnCount;

    [SerializeField]
    public float turnDeficit = 0;

    [SerializeField]
    public float turnProfit = 0;

    private void CalculateBuildingDeficit()
    {
        turnDeficit = 0;
        turnProfit = 0;

        foreach (BuildingProperties building in FileHandlerStory.Instance.gameData.buildingsList)
        {
            turnDeficit += building.costPerTurn;
            turnProfit = turnProfit + (building.addingValue * building.valueModifier);
        }

    }

    public void FinishTurn()
    {
        CalculateBuildingDeficit();
        Debug.Log(turnDeficit);
        FileHandlerStory.Instance.gameData.AddMoney((int)turnProfit);
        FileHandlerStory.Instance.gameData.SubtractMoney((int)turnDeficit);

        // Incrementar el turno en EconomyTracker
        EconomyTracker.Instance.IncrementTurn();
    }
}
