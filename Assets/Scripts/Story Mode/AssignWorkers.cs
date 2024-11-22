using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AssignWorkers : MonoBehaviour
{
    [SerializeField] private TMP_InputField workersInputField;
    [SerializeField] private Button applyButton;
    private BuildingManager targetBuilding;

    private void Start()
    {
        if (workersInputField != null)
        {
            workersInputField.onValueChanged.AddListener(ValidateInput);
        }
        
        if (applyButton != null)
        {
            applyButton.onClick.AddListener(ApplyWorkers);
        }
    }

    public void SetTargetBuilding(BuildingManager building)
    {
        targetBuilding = building;
        if (targetBuilding != null)
        {
            // Find the current workers number for this building
            foreach (BuildingProperties buildingData in FileHandlerStory.Instance.gameData.buildingsList)
            {
                if (buildingData.x == building.transform.position.x && 
                    buildingData.z == building.transform.position.z)
                {
                    workersInputField.text = buildingData.workersNum.ToString();
                    break;
                }
            }
        }
    }

    private void ValidateInput(string value)
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

    private void ApplyWorkers()
    {
        if (targetBuilding != null && int.TryParse(workersInputField.text, out int workers))
        {
            targetBuilding.SetWorkers(workers);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
