using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlaceBuilding : MonoBehaviour
{
    private GameObject buildingPreviewPrefab;
    private GameObject buildingToBuild;
    private GameObject newPreview;
    private bool isPreviewActive = false;
    private GameModel gameProperties;

    public void Start()
    {
        gameProperties = GameObject.FindGameObjectWithTag("GameProp").GetComponent<GameModel>();
        Debug.Log(gameProperties.money);
    }

    public void BeginBuildingPreview(int buildingOption)
    {
        switch (buildingOption)
        {
            case 0:
                buildingPreviewPrefab = Resources.Load<GameObject>("Prefabs/Edificios/Horno1HoverPrefab");
                if (gameProperties.money >= buildingPreviewPrefab.GetComponent<BuildingProperties>().costToBuild)
                {
                    isPreviewActive = true;
                    buildingToBuild = Resources.Load<GameObject>("Prefabs/Edificios/Horno 1");
                    newPreview = Instantiate(buildingPreviewPrefab, new Vector3(0f + buildingPreviewPrefab.transform.position.x, 0f + buildingPreviewPrefab.transform.position.y, 0f + buildingPreviewPrefab.transform.position.z), Quaternion.identity);

                    foreach (GameObject button in ButtonManager.toolbarButtons)
                    {
                        button.GetComponent<Button>().interactable = false;
                    }
                }
                else
                {
                    Debug.Log("Cant afford");
                }
                break;
            case 1:
                buildingPreviewPrefab = Resources.Load<GameObject>("Prefabs/Edificios/AlmacenMateriaPrimaHoverPrefab");
                if (gameProperties.money >= buildingPreviewPrefab.GetComponent<BuildingProperties>().costToBuild)
                {
                    isPreviewActive = true;

                    buildingToBuild = Resources.Load<GameObject>("Prefabs/Edificios/Almacén de Materia Prima");
                    newPreview = Instantiate(buildingPreviewPrefab, new Vector3(0f + buildingPreviewPrefab.transform.position.x, 0f + buildingPreviewPrefab.transform.position.y, 0f + buildingPreviewPrefab.transform.position.z), Quaternion.identity);

                    foreach (GameObject button in ButtonManager.toolbarButtons)
                    {
                        button.GetComponent<Button>().interactable = false;
                    }
                }
                else
                {
                    Debug.Log("Cant afford");
                }
                break;
        }
        
    }

    public void OnClickKey(InputAction.CallbackContext ctx)
    {
        Debug.Log(isPreviewActive);
        if (isPreviewActive)
        {
            bool canBuild = newPreview.GetComponent<PlacingTest>().canBuild;
            bool clickKeyPressed = ctx.started;
            if (clickKeyPressed && canBuild)
            {
                GameObject placedBuilding = Instantiate(buildingToBuild, newPreview.transform.position, newPreview.transform.rotation);
                gameProperties.buildingsInMap.Add(placedBuilding);
                isPreviewActive = false;
                foreach (GameObject button in ButtonManager.toolbarButtons)
                {
                    button.GetComponent<Button>().interactable = true;
                }
                Debug.Log("Destruir");
                Destroy(newPreview);
                gameProperties.SubtractMoney(placedBuilding.GetComponent<BuildingProperties>().costToBuild);
                
            }
        }
    }

    public void OnQkey(InputAction.CallbackContext ctx)
    {
        if (isPreviewActive)
        {
            bool qKeyPressed = ctx.started;

            if (qKeyPressed)
            {
                newPreview.transform.rotation = Quaternion.Euler(newPreview.transform.rotation.eulerAngles.x, newPreview.transform.rotation.eulerAngles.y - 90f, newPreview.transform.rotation.eulerAngles.z);
            }
        }
    }

    public void OnEkey(InputAction.CallbackContext ctx)
    {
        if (isPreviewActive)
        {
            bool eKeyPressed = ctx.started;

            if (eKeyPressed)
            {
                newPreview.transform.rotation = Quaternion.Euler(newPreview.transform.rotation.eulerAngles.x, newPreview.transform.rotation.eulerAngles.y + 90f, newPreview.transform.rotation.eulerAngles.z);
            }
        }
    }
}
