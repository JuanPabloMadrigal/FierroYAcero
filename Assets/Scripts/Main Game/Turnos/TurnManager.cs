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
    public int buildingsMaintainanceCost;

    public int moneyToSubtract;
    public int moneyToAdd;

    public int coqueToAdd = 0;
    public int ironToAdd = 0;
    public int coqueToSubtract = 0;
    public int ironToSubtract = 0;
    public int steelToAdd = 0;
    public int steelBarToAdd = 0;
    public int railToAdd = 0;
    public int steelToSubtract = 0;
    public int steelBarToSubtract = 0;
    public int railToSubtract = 0;

    [SerializeField] SceneInitialization sceneInitialization;

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

        foreach (BuildingProperties building in FileHandlerStory.Instance.gameData.buildingsList)
        {
            buildingsMaintainanceCost += building.costPerTurn;
        }

    }


    // int finalProduct = Mathf.RoundToInt(((ironQuantity * building.addingValue * building.valueModifier * building.workersNum / 100)) / productCost);

    public void FinishTurn()
    {

        CalculateBuildingDeficit();
        moneyToSubtract += buildingsMaintainanceCost;
        if (FileHandlerStory.Instance.gameData.turnsWithoutSalary > 2) { 
            moneyToSubtract += FileHandlerStory.Instance.gameData.salaryAmount;
            FileHandlerStory.Instance.gameData.turnsWithoutSalary = 0;
        }

        Debug.Log($"Costo de mant.: {moneyToSubtract}");

        SpawnNPCsForAllBuildings();

        // Se agregan los cambios al modelo del juego según las acciones en el turno


        FileHandlerStory.Instance.gameData.AddMoney((int)moneyToAdd);
        FileHandlerStory.Instance.gameData.SubtractMoney((int)moneyToSubtract);

        FileHandlerStory.Instance.gameData.AddCoque(coqueToAdd);
        FileHandlerStory.Instance.gameData.AddIron(ironToAdd);
        FileHandlerStory.Instance.gameData.SubtractCoque(coqueToSubtract);
        FileHandlerStory.Instance.gameData.SubtractIron(ironToSubtract);

        FileHandlerStory.Instance.gameData.AddSteel(steelToAdd);
        FileHandlerStory.Instance.gameData.AddSteelBar(steelBarToAdd);
        FileHandlerStory.Instance.gameData.AddRail(railToAdd);
        FileHandlerStory.Instance.gameData.SubtractSteel(steelToSubtract);
        FileHandlerStory.Instance.gameData.SubtractSteelBar(steelBarToSubtract);
        FileHandlerStory.Instance.gameData.SubtractRail(railToSubtract);


        // Incrementar el turno en EconomyTracker
        EconomyTracker.Instance.IncrementTurn();

        Debug.Log("Termina el turno");

        StartCoroutine(sceneInitialization.RestartChildBuildings());

        if (FileHandlerStory.Instance.gameData.evento == 1)
        {
            FileHandlerStory.Instance.gameData.evento++;
        }

        coqueToAdd = 0;
        ironToAdd = 0;
        coqueToSubtract = 0;
        ironToSubtract = 0;
        steelToAdd = 0;
        steelBarToAdd = 0;
        railToAdd = 0;
        steelToSubtract = 0;
        steelBarToSubtract = 0;
        railToSubtract = 0;

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
