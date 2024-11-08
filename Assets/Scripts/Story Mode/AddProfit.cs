using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AddProfit : MonoBehaviour
{
    public void AddTest()
    {
        FileHandlerStory.Instance.gameData.AddMoney(1000);
        foreach (BuildingProperties buildingData in FileHandlerStory.Instance.gameData.buildingsList)
        {
            buildingData.AddModifier(0.2f);
        }
        Debug.Log(FileHandlerStory.Instance.gameData.money);
    }

    public void SaveGame()
    {
        FileHandlerStory.Instance.WriteFile();
    }

    public void LoadGame()
    {
        FileHandlerStory.Instance.ReadFile();
        UIManager.Instance.UpdateMoneyUI(FileHandlerStory.Instance.gameData.money);
        UIManager.Instance.UpdateCoqueUI(FileHandlerStory.Instance.gameData.coque);
    }

}
