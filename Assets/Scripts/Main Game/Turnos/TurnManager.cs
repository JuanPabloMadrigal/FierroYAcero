using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
    private int turnCount;

    [SerializeField] 
    private int turnDeficit = 0;

    public TMP_Text moneyUI;
    public static int money = 2000;



    // Start is called before the first frame update
    private void Start()
    {
        moneyUI.text = money.ToString();
    }

    private void UpdateMoney()
    {
        moneyUI.text = money.ToString();
    }

    private void CalculateBuildingDeficit()
    {
        
        foreach (GameObject building in PlaceBuilding.buildingsInMap) 
        {
            turnDeficit += building.GetComponent<BuildingProperties>().costPerTurn;
        }
        
    }

    public void FinishTurn()
    {
        CalculateBuildingDeficit();
        Debug.Log(turnDeficit);
        money -= turnDeficit;
        UpdateMoney();
    }
}
