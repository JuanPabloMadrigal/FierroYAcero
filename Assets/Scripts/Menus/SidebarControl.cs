using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidebarControl : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject UIPrefabCoque;
    [SerializeField] private GameObject UIPrefabHorno1;

    private GameObject currentUI;

    [SerializeField] private Animator sidebarAnimator;
    [SerializeField] private string openTrigger = "Open";
    [SerializeField] private string closeTrigger = "Close";

    private bool isOpen = false;


    private GameObject currentBuildingUI;


    public void OpenSideBar(string building)
    {
        if (!isOpen) 
        {
            sidebarAnimator.SetTrigger(openTrigger);
            PopulateSideBar(building);
            isOpen = true;
        }
        
        PopulateSideBar(building);

    }

    public void CloseSideBar()
    {
        sidebarAnimator.SetTrigger(closeTrigger);
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

        }
    }
    
}
