using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static GameObject[] toolbarButtons;
    // Start is called before the first frame update
    void Start()
    {
        toolbarButtons = GameObject.FindGameObjectsWithTag("BuildingButton");
    }


}
