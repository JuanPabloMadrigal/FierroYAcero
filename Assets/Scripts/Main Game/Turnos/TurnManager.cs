using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TurnManager : MonoBehaviour
{

    private GameModel gameProperties;

    private int turnCount;

    [SerializeField] 
    private int turnDeficit = 0;

    [SerializeField]
    private int turnProfit = 0;
    private void Start()
    {
        gameProperties = GameObject.FindGameObjectWithTag("GameProp").GetComponent<GameModel>();
    }

    private void CalculateBuildingDeficit()
    {
        
        foreach (GameObject building in gameProperties.buildingsInMap) 
        {
            turnDeficit += building.GetComponent<BuildingProperties>().costPerTurn;
            turnProfit += building.GetComponent<BuildingProperties>().profitPerTurn;
        }
        
    }

    public void FinishTurn()
    {
        CalculateBuildingDeficit();
        Debug.Log(turnDeficit);
        gameProperties.AddMoney(turnProfit);
        gameProperties.SubtractMoney(turnDeficit);
    }
}
