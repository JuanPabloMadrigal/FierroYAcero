using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AssignWorkers : MonoBehaviour
{
    [SerializeField] private TMP_InputField workersInputField;
    [SerializeField] private Button applyButton;
    public GameObject targetBuilding;
    private BuildingProperties building;

    public void SetTargetBuilding()
    {
        if (targetBuilding != null)
        {
            // Find the current workers number for this building
            foreach (BuildingProperties buildingData in FileHandlerStory.Instance.gameData.buildingsList)
            {
                if (buildingData.x == targetBuilding.transform.position.x &&
                    buildingData.z == targetBuilding.transform.position.z)
                {
                    building = buildingData;
                    break;
                }
            }
        }
    }

    public void ValidateInput(string value)
    {
        // Only allow positive numbers
        if (string.IsNullOrEmpty(value))
        {
            applyButton.interactable = false;
            return;
        }

        if (int.TryParse(value, out int workers))
        {
            applyButton.interactable = workers >= 0;
        }
        else
        {
            applyButton.interactable = false;
        }
    }

    public void ApplyWorkers()
    {
        SetTargetBuilding();
        if (building != null && int.TryParse(workersInputField.text, out int workers))
        {
            building.workersNum = workers;
        }
        foreach (BuildingProperties buildingData in FileHandlerStory.Instance.gameData.buildingsList)
        {
            Debug.Log(buildingData.workersNum + "" + buildingData.type); 
        }
    }
}
