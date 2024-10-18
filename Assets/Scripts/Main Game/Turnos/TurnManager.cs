using System;
using System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TurnManager : MonoBehaviour
{
    private int turnCount;

    [SerializeField]
    private float turnDeficit = 0;

    [SerializeField]
    private float turnProfit = 0;

    private void CalculateBuildingDeficit()
    {

        foreach (BuildingProperties building in FileHandler.Instance.gameData.buildingsList)
        {
            turnDeficit += building.costPerTurn;
            turnProfit = turnProfit + (building.addingValue * building.valueModifier);
        }

    }

    public void FinishTurn()
    {
        CalculateBuildingDeficit();
        Debug.Log(turnDeficit);
        FileHandler.Instance.gameData.AddMoney((int)turnProfit);
        FileHandler.Instance.gameData.SubtractMoney((int)turnDeficit);
    }
}
