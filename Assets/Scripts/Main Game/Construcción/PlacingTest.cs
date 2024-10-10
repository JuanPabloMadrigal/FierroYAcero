using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlacingTest : MonoBehaviour
{
    public float heightAboveGround = 0f; // Adjust this value to set the height of the object
    public LayerMask groundLayer; // Set this in the inspector to define what layers are considered "ground"
    public GameObject canBuildBuilding;
    public GameObject cannotBuildBuilding;
    public GameObject building;
    public bool canBuild;

    private float original_x;
    private float original_y;
    private float original_z;

    // Start is called before the first frame update
    void Start()
    {
        canBuildBuilding.SetActive(true);
        cannotBuildBuilding.SetActive(false);
        canBuild = true;

        original_x = transform.position.x;
        original_y = transform.position.y;
        original_z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Vector3 targetPosition = new Vector3((float)(int)hit.point.x + original_x, 0 + original_y, (float)(int)hit.point.z + original_z) + Vector3.up * heightAboveGround;
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
        canBuild = false;
    }

    private void AllowBuild()
    {
        canBuildBuilding.SetActive(true);
        cannotBuildBuilding.SetActive(false);
        canBuild = true;
    }


}
