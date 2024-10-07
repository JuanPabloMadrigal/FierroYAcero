using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Moving : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWayPointIndex;
    public float speed = 10.0f;

    private float waitTime = 1.0f;
    private float waitCounter = 0.0f;
    private bool waiting = false;
    private Vector3 wpTransf;

    // Update is called once per frame
    void Update()
    {
        if(waiting)
        {
            waitCounter += Time.deltaTime;
            if(waitCounter<waitTime)
                return;
                waiting = false;
        }
        Transform wp = waypoints[currentWayPointIndex];
        wpTransf = new Vector3(wp.position.x, transform.position.y, wp.position.z);
        if(Vector3.Distance(transform.position, wpTransf)< 0.01f)
        {
            transform.position = wpTransf;
            currentWayPointIndex = (currentWayPointIndex + 1) % waypoints.Length;
            waiting = true;
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position, wpTransf, speed * Time.deltaTime);
            transform.LookAt(wpTransf);
        }
    }
}
