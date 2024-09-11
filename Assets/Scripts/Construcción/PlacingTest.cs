using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlacingTest : MonoBehaviour
{
    public float heightAboveGround = 0f; // Adjust this value to set the height of the object
    public LayerMask groundLayer; // Set this in the inspector to define what layers are considered "ground"
    public GameObject canBuildBuilding;
    public GameObject cannotBuildBuilding;
    
    private GameObject logic;
    private PlacingWithModelTest buildBuildingScript;

    // Start is called before the first frame update
    void Start()
    {
        canBuildBuilding.SetActive(true);
        cannotBuildBuilding.SetActive(false);
        logic = GameObject.Find("Logic");
        buildBuildingScript = logic.GetComponent<PlacingWithModelTest>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Vector3 targetPosition = new Vector3((float)(int)hit.point.x, 0, (float)(int)hit.point.z) + Vector3.up * heightAboveGround;
            transform.position = targetPosition;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        PreventBuild();
    }

    void OnTriggerStay(Collider other)
    {
        PreventBuild();
    }

    private void OnTriggerExit(Collider other)
    {
        AllowBuild();
    }

    private void PreventBuild()
    {
        canBuildBuilding.SetActive(false);
        cannotBuildBuilding.SetActive(true);
        buildBuildingScript.canBuild = false;
    }

    private void AllowBuild()
    {
        canBuildBuilding.SetActive(true);
        cannotBuildBuilding.SetActive(false);
        buildBuildingScript.canBuild = true;
    }


}
