using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class StoryTurnManager : MonoBehaviour
{

    private GameModel gameProperties;

    private int turnCount;

    [SerializeField]
    private int turnDeficit = 0;

    [SerializeField]
    private int turnProfit = 0;

    /*[SerializeField]
    public int money = 1000;*/

    // Start is called before the first frame update
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
