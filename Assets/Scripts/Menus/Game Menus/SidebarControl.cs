using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SidebarControl : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject UIPrefabCoque;
    [SerializeField] private GameObject UIPrefabHorno1;
    [SerializeField] private GameObject UIPrefabAlmacenHierro;
    [SerializeField] private GameObject UIPrefabAceracion;
    [SerializeField] private GameObject UIPrefabTrabajadores;
    public GameObject buttonGameObject;

    private GameObject currentUI;

    [SerializeField] private Animator sidebarAnimator;
    [SerializeField] private Animator secondSidebarAnimator;
    [SerializeField] private GameObject secondPanel;
    [SerializeField] private string openTrigger = "Open";
    [SerializeField] private string closeTrigger = "Close";

    private bool isOpen = false;


    private GameObject currentWorkerUI;


    public void OpenSideBar(string building)
    {
        if (!isOpen)
        {
            sidebarAnimator.SetTrigger(openTrigger);
            secondSidebarAnimator.SetTrigger(openTrigger);
            PopulateSideBar(building);
            PopulateWorkerSideBar(building);
            isOpen = true;
        }

        PopulateSideBar(building);

    }

    public void OnButtonClicked(Button button)
    {
        buttonGameObject = button.gameObject;
    }

    public void CloseSideBar()
    {
        sidebarAnimator.SetTrigger(closeTrigger);
        secondSidebarAnimator.SetTrigger(closeTrigger);
        isOpen = false;
        Destroy(currentUI);
    }

    public void PopulateSideBar(string building)
    {
        switch (building)
        {
            case "plantadecoque":
                if (currentUI != null)
                {
                    Destroy(currentUI);
                }
                currentUI = Instantiate(UIPrefabCoque);
                currentUI.transform.SetParent(gameObject.transform, false);
                break;
            case "horno1":
                if (currentUI != null)
                {
                    Destroy(currentUI);
                }
                currentUI = Instantiate(UIPrefabHorno1);
                currentUI.transform.SetParent(gameObject.transform, false);
                break;
            case "hierro":
                if (currentUI != null)
                {
                    Destroy(currentUI);
                }
                currentUI = Instantiate(UIPrefabAlmacenHierro);
                currentUI.transform.SetParent(gameObject.transform, false);
                break;
            case "aceracion":
                if (currentUI != null)
                {
                    Destroy(currentUI);
                }
                currentUI = Instantiate(UIPrefabAceracion);
                currentUI.transform.SetParent(gameObject.transform, false);
                break;
        }
    }

    public void PopulateWorkerSideBar(string building)
    {
        switch (building)
        {
            case "plantadecoque":
                if (currentWorkerUI != null)
                {
                    Destroy(currentWorkerUI);
                }
                currentWorkerUI = Instantiate(UIPrefabTrabajadores);
                currentWorkerUI.transform.SetParent(secondPanel.transform, false);
                //currentWorkerUI.GetComponent<AssignWorkers>().targetBuilding = buttonGameObject;
                break;
            case "horno1":
                if (currentWorkerUI != null)
                {
                    Destroy(currentWorkerUI);
                }
                currentWorkerUI = Instantiate(UIPrefabTrabajadores);
                currentWorkerUI.transform.SetParent(secondPanel.transform, false);
                //currentWorkerUI.GetComponent<AssignWorkers>().targetBuilding = buttonGameObject;
                break;
            case "hierro":
                if (currentWorkerUI != null)
                {
                    Destroy(currentWorkerUI);
                }
                currentWorkerUI = Instantiate(UIPrefabTrabajadores);
                currentWorkerUI.transform.SetParent(secondPanel.transform, false);
                //currentWorkerUI.GetComponent<AssignWorkers>().targetBuilding = buttonGameObject;
                break;
            case "aceracion":
                if (currentWorkerUI != null)
                {
                    Destroy(currentWorkerUI);
                }
                currentWorkerUI = Instantiate(UIPrefabTrabajadores);
                currentWorkerUI.transform.SetParent(secondPanel.transform, false);
                //currentWorkerUI.GetComponent<AssignWorkers>().targetBuilding = buttonGameObject;
                break;
        }
    }

}