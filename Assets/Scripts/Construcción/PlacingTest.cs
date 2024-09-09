using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlacingTest : MonoBehaviour
{
    [SerializeField] private float heightAboveGround = 0f; // Adjust this value to set the height of the object
    [SerializeField] private LayerMask groundLayer; // Set this in the inspector to define what layers are considered "ground"

    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Vector3 targetPosition = hit.point + Vector3.up * heightAboveGround;
            transform.position = targetPosition;
        }

    }
}
