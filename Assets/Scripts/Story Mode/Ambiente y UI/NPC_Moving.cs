using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Moving : MonoBehaviour
{
    public Transform[] destinations;
    [SerializeField]private int currentIndex;

    private float waitTime = 10.0f;
    [SerializeField]private float waitCounter = 0.0f;
    [SerializeField]private bool waiting;
    private Vector3 desTransf;
    [SerializeField]private NavMeshAgent navmeshAgent;

    void Start()
    {
        navmeshAgent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(waiting)
        {
            waitCounter += Time.deltaTime;
            if(waitCounter > waitTime)
            {
                waiting = false;
                waitCounter = 0.0f;
                return;
            }
        }
        else{
            Transform des = destinations[currentIndex];
            Vector3 desTransf = new Vector3(des.position.x, transform.position.y, des.position.z);
            if(Vector3.Distance(transform.position, desTransf)< 0.1f)
            {
                transform.position = desTransf;
                currentIndex = (currentIndex + 1) % destinations.Length;
                waiting = true;
            }
            else{
                SetDestination(desTransf);
            }
        }
    }

    private void SetDestination(Vector3 destiny)
    { 
        navmeshAgent.SetDestination(destiny);
    }
}
