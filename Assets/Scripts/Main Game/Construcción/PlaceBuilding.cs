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

    private GameObject[] buildingButtons;
    static public List<GameObject> buildingsInMap;

    public void Start()
    {
        buildingButtons = GameObject.FindGameObjectsWithTag("BuildingButton");
        buildingsInMap = new List<GameObject>();
    }

    public void BeginBuildingPreview(int buildingOption)
    {
        switch (buildingOption)
        {
            case 0:
                isPreviewActive = true;
                buildingPreviewPrefab = Resources.Load<GameObject>("Prefabs/Edificios/Horno1HoverPrefab");
                buildingToBuild = Resources.Load<GameObject>("Prefabs/Edificios/Horno 1");
                newPreview = Instantiate(buildingPreviewPrefab, new Vector3(0f + buildingPreviewPrefab.transform.position.x, 0f + buildingPreviewPrefab.transform.position.y, 0f + buildingPreviewPrefab.transform.position.z), Quaternion.identity);
                
                foreach(GameObject button in buildingButtons)
                {
                    button.GetComponent<Button>().interactable = false;
                }

                break;
            case 1:
                isPreviewActive = true;
                buildingPreviewPrefab = Resources.Load<GameObject>("Prefabs/Edificios/AlmacenMateriaPrimaHoverPrefab");
                buildingToBuild = Resources.Load<GameObject>("Prefabs/Edificios/Almacén de Materia Prima");
                newPreview = Instantiate(buildingPreviewPrefab, new Vector3(0f + buildingPreviewPrefab.transform.position.x, 0f + buildingPreviewPrefab.transform.position.y, 0f + buildingPreviewPrefab.transform.position.z), Quaternion.identity);
                
                foreach (GameObject button in buildingButtons)
                {
                    button.GetComponent<Button>().interactable = false;
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
                buildingsInMap.Add(placedBuilding);
                isPreviewActive = false;
                foreach (GameObject button in buildingButtons)
                {
                    button.GetComponent<Button>().interactable = true;
                }
                Destroy(newPreview);
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
