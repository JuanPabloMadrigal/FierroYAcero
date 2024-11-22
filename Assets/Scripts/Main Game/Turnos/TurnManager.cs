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

    public int moneyToSubtract;
    public int moneyToAdd;

    public int coqueToAdd = 0;
    public int ironToAdd = 0;
    public int coqueToSubtract = 0;
    public int ironToSubtract = 0;
    public int steelToAdd = 0;
    public int steelBarToAdd = 0;
    public int railToAdd = 0;

    public static TurnManager Instance;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void CalculateBuildingDeficit()
    {
        turnDeficit = 0;

        foreach (BuildingProperties building in FileHandlerStory.Instance.gameData.buildingsList)
        {
            turnDeficit += building.costPerTurn;
        }

    }


    // int finalProduct = Mathf.RoundToInt(((ironQuantity * building.addingValue * building.valueModifier * building.workersNum / 100)) / productCost);

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
