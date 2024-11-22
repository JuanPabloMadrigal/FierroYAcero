using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IndicatorAnimation : MonoBehaviour
{

    [SerializeField] private float minLimit = -2 ;
    [SerializeField] private float maxLimit = 2;
    //private Vector3 currPos = Vector3.zero;
    [SerializeField] private bool movement = false;
    [SerializeField] private Vector3 velocity = Vector3.zero;
    [SerializeField] private float speed;

    private void Start()
    {
        //transform.Translate(new Vector3(0, -0.1f, 0));
    }

    private void FixedUpdate()
    {

        if (transform.position.y > maxLimit - 0.1f || transform.position.y < minLimit + 0.1f)
        {
            movement = !movement;
        }

        if (movement)
        {
            transform.position = Vector3.SmoothDamp(transform.position, Vector3.zero + new Vector3(transform.position.x, maxLimit, transform.position.z), ref velocity, speed * Time.deltaTime);
        } 
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, Vector3.zero + new Vector3(transform.position.x, minLimit, transform.position.z), ref velocity, speed * Time.deltaTime);
        }
    }
}
