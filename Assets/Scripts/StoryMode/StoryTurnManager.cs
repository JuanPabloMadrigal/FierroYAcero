using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class StoryTurnManager : MonoBehaviour
{
    public static StoryTurnManager Instance;

    private int turnCount;

    [SerializeField] 
    private int turnDeficit = 0;

    [SerializeField]
    private int turnProfit = 0;

    public TMP_Text moneyUI;

    [SerializeField]
    public int money = 1000;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {

        moneyUI.text = money.ToString();
    }

    public void UpdateMoney()
    {
        moneyUI.text = money.ToString();
    }

    private void CalculateBuildingDeficit()
    {
        
        foreach (GameObject building in PlaceBuilding.buildingsInMap) 
        {
            turnDeficit += building.GetComponent<BuildingProperties>().costPerTurn;
            turnProfit += building.GetComponent<BuildingProperties>().profitPerTurn;
        }
        
    }

    public void FinishTurn()
    {
        CalculateBuildingDeficit();
        Debug.Log(turnDeficit);
        money += turnProfit;
        money -= turnDeficit;
        UpdateMoney();
    }
}
