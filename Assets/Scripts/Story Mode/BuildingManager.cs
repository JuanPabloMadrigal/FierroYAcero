using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private Vector3 buildingPos;

    private void Start()
    {
        buildingPos = transform.position;
    }

    public void SetWorkers(int workers)
    {
        foreach (BuildingProperties buildingData in FileHandlerStory.Instance.gameData.buildingsList)
        {
            if (buildingPos.x == buildingData.x && buildingPos.z == buildingData.z)
            {
                buildingData.workersNum = workers;
            }
        }
    }
}
