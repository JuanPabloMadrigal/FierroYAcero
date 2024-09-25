using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceBuilding : MonoBehaviour
{
    public GameObject buildingPreviewPrefab;
    public GameObject buildingToBuild;

    private GameObject newPreview;
    public bool isPreviewActive = false;

    public void BeginBuildingPreview(int buildingOption)
    {
        switch (buildingOption)
        {
            case 0:
                buildingPreviewPrefab = Resources.Load<GameObject>("Prefabs/Edificios/Horno1HoverPrefab");
                buildingToBuild = Resources.Load<GameObject>("Prefabs/Edificios/Horno 1");
                newPreview = Instantiate(buildingPreviewPrefab, new Vector3(0f, 0f, 8f), Quaternion.identity);
                isPreviewActive = true;
                break;
            case 1:
                buildingPreviewPrefab = Resources.Load<GameObject>("Prefabs/Edificios/AlmacenMateriaPrimaHoverPrefab");
                buildingToBuild = Resources.Load<GameObject>("Prefabs/Edificios/Almacén de Materia Prima");
                newPreview = Instantiate(buildingPreviewPrefab, new Vector3(0f, 0f, 8f), Quaternion.identity);
                isPreviewActive = true;
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
                GameObject placedBuilding = Instantiate(buildingToBuild, newPreview.transform.position, newPreview.transform.rotation);
                isPreviewActive = false;
                Destroy(newPreview);
            }
        }
    }
}
