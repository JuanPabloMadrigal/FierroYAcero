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

    public void Start()
    {
        buildingButtons = GameObject.FindGameObjectsWithTag("BuildingButton");
    }


    public void BeginBuildingPreview(int buildingOption)
    {
        switch (buildingOption)
        {
            case 0:
                buildingPreviewPrefab = Resources.Load<GameObject>("Prefabs/Edificios/Horno1HoverPrefab");
                buildingToBuild = Resources.Load<GameObject>("Prefabs/Edificios/Horno 1");
                newPreview = Instantiate(buildingPreviewPrefab, new Vector3(0f + buildingPreviewPrefab.transform.position.x, 0f + buildingPreviewPrefab.transform.position.y, 0f + buildingPreviewPrefab.transform.position.z), Quaternion.identity);
                isPreviewActive = true;

                foreach(GameObject button in buildingButtons)
                {
                    button.GetComponent<Button>().interactable = false;
                }

                break;
            case 1:
                buildingPreviewPrefab = Resources.Load<GameObject>("Prefabs/Edificios/AlmacenMateriaPrimaHoverPrefab");
                buildingToBuild = Resources.Load<GameObject>("Prefabs/Edificios/Almac�n de Materia Prima");
                newPreview = Instantiate(buildingPreviewPrefab, new Vector3(0f + buildingPreviewPrefab.transform.position.x, 0f + buildingPreviewPrefab.transform.position.y, 0f + buildingPreviewPrefab.transform.position.z), Quaternion.identity);
                isPreviewActive = true;

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
            Debug.Log("previewactive");
            bool canBuild = newPreview.GetComponent<PlacingTest>().canBuild;
            bool clickKeyPressed = ctx.started;
            if (clickKeyPressed && canBuild)
            {
                Debug.Log("Click Izq. Detectado");
                Debug.Log(newPreview.transform.position);
                GameObject placedBuilding = Instantiate(buildingToBuild, newPreview.transform.position, newPreview.transform.rotation);
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
