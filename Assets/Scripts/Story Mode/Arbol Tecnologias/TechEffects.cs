using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class TechEffects : MonoBehaviour
{
    public TechTree techTree;

    void Start()
    {
        // Subscribe to the OnTechUnlocked event
        techTree.OnTechUnlocked += HandleTechUnlocked;
    }

    void HandleTechUnlocked(string techName)
    {
        switch (techName)
        {
            // Resource Management
            case "Almacén de materia prima":
                UnlockResourcePurchasing();
                break;
            case "Planta de coque":
                UnlockCokeProduction();
                break;

            // Furnace Improvements
            case "Horno 2":
                ImproveFurnaceEfficiency();
                break;
            case "Horno 3":
                ImproveFurnaceEfficiency();
                break;

            // Steel Workshop Improvements
            case "Taller de aceración 1":
                UnlockSteelingPlant(1);
                break;
            case "Taller de aceración 2":
                UnlockSteelingPlant(2);
                break;
            case "Taller de aceración 3":
                UnlockSteelingPlant(3);
                break;
            case "Taller de aceración 4":
                UnlockSteelingPlant(4);
                break;
            case "Taller de aceración 5":
                UnlockSteelingPlant(5);
                break;
            case "Taller de aceración 6":
                UnlockSteelingPlant(6);
                break;
            case "Taller de aceración 7":
                UnlockSteelingPlant(7);
                break;

            // Worker Management
            case "Trabajadores 1":
                ReduceWorkerCosts();
                break;
            case "Trabajadores 2":
                ReduceWorkerCosts();
                break;
            case "Trabajadores 3":
                ReduceWorkerCosts();
                break;
            case "Trabajadores 4":
                ReduceWorkerCosts();
                break;

            // Negotiation Skills
            case "Negociación 1":
                ImproveSellingPrice();
                break;
            case "Negociación 2":
                ImproveSellingPrice();
                break;
            case "Negociación 3":
                ImproveSellingPrice();
                break;
            case "Negociación 4":
                ImproveSellingPrice();
                break;
            case "Negociación 5":
                ImproveSellingPrice();
                break;

            // Infrastructure Improvements
            case "Infraestructura 1":
                ReduceMaintenanceCosts();
                break;
            case "Infraestructura 2":
                ReduceMaintenanceCosts();
                break;
            case "Infraestructura 3":
                ReduceMaintenanceCosts();
                break;
            case "Infraestructura 4":
                ReduceMaintenanceCosts();
                break;
            case "Infraestructura 5":
                ReduceMaintenanceCosts();
                break;

            default:
                Debug.Log($"Technology unlocked: {techName}");
                break;
        }
    }

    // Helper methods that would need to be implemented
    private void UnlockResourcePurchasing()
    {
        // Enable iron and coal purchasing in the game
        //var index = FileHandlerStory.Instance.gameData.buildingsList.FindIndex(i => i.type == "Almacen MP");

        FileHandlerStory.Instance.gameData.ironStorehouse.unlocked = true;
    }

    private void UnlockCokeProduction()
    {
        FileHandlerStory.Instance.gameData.cokePlant.unlocked = true;
    }

    private void ImproveFurnaceEfficiency()
    {
        // Improve furnace production rates
        
    }

    private void UnlockNewProducts()
    {
        // Unlock new product types for production
       
    }

    private void UnlockAdvancedProducts()
    {
        // Unlock advanced product types
       
    }

    private void UnlockSteelingPlant(int num)
    {
        // Desbloquear taller de aceración indicado

        FileHandlerStory.Instance.gameData.buildingsList[num + 3].unlocked = true;
        Debug.Log($"Effect done! The steeling workshop in position {num + 3} is {FileHandlerStory.Instance.gameData.buildingsList[num + 3].unlocked}.");

    }

    private void ReduceWorkerCosts()
    {
        // Reduce worker salary costs
        
    }

    private void ImproveSellingPrice()
    {
        // Improve selling prices for all products
        
    }

    private void ReduceMaintenanceCosts()
    {
        // Reduce building maintenance costs
        
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed
        if (techTree != null)
        {
            techTree.OnTechUnlocked -= HandleTechUnlocked;
        }
    }
}
