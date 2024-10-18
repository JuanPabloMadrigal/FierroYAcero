using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddProfit : MonoBehaviour
{
    public void AddTest()
    {
        FileHandlerStory.Instance.gameData.money += 100;
        foreach (BuildingProperties buildingData in FileHandlerStory.Instance.gameData.buildingsList)
        {
            buildingData.AddModifier(0.2f);
        }
        UIManager.Instance.UpdateMoneyUI(FileHandlerStory.Instance.gameData.money);
    }

    public void SaveGame()
    {
        FileHandlerStory.Instance.WriteFile();
    }

    public void LoadGame()
    {
        FileHandlerStory.Instance.ReadFile();
    }

}
