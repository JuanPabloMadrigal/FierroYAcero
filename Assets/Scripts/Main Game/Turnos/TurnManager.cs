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
        SpawnNPCsForAllBuildings();
        Debug.Log(turnDeficit);
        FileHandlerStory.Instance.gameData.AddMoney((int)turnProfit);
        FileHandlerStory.Instance.gameData.SubtractMoney((int)turnDeficit);
    }

    private void SpawnNPCsForAllBuildings()
    {
        Debug.Log("Starting to spawn NPCs for all buildings");
        if (NPCSpawner.Instance == null)
        {
            Debug.LogError("NPCSpawner.Instance is null! Make sure NPCSpawner exists in the scene.");
            return;
        }

        // Clear existing NPCs before spawning new ones
        NPCSpawner.Instance.ClearExistingNPCs();

        foreach (BuildingProperties building in FileHandlerStory.Instance.gameData.buildingsList)
        {
            Vector3 buildingPosition = new Vector3(building.x, 0, building.z);
            Debug.Log($"Spawning NPCs for building at {buildingPosition} with {building.workersNum} workers");
            NPCSpawner.Instance.SpawnNPCsForBuilding(buildingPosition, building.workersNum);
        }
    }
}
