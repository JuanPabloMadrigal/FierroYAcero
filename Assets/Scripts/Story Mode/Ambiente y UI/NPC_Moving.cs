using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Moving : MonoBehaviour
{
    public Transform[] destinations;
    [SerializeField] private int currentIndex;
    private float arrivalDistance = 6f;
    
    [SerializeField] private NavMeshAgent navmeshAgent;
    [SerializeField] private GameObject childNPC;
    public System.Action OnReachedDestination;
    private bool hasReachedDestination = false;

    void Start()
    {
        navmeshAgent = this.GetComponent<NavMeshAgent>();
        navmeshAgent.speed = UnityEngine.Random.Range(3.0f, 4.0f);
        
        if (destinations != null && destinations.Length > 0)
        {
            SetDestination(destinations[0].position);
        }
    }

    void Update()
    {
        if (hasReachedDestination) return;
        
        if (destinations == null || destinations.Length == 0) return;

        float distanceToTarget = Vector3.Distance(transform.position, destinations[0].position);
        
        if (distanceToTarget < arrivalDistance && !hasReachedDestination)
        {
            Debug.Log($"NPC reached destination at distance: {distanceToTarget}");
            hasReachedDestination = true;
            OnReachedDestination?.Invoke();
        }
    }

    private void SetDestination(Vector3 destiny)
    { 
        navmeshAgent.SetDestination(destiny);
    }
}
