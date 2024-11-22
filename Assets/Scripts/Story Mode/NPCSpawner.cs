using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public static NPCSpawner Instance;
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private Transform spawnPoint;
    private Vector3 defaultSpawnPosition = new Vector3(-8f, 0f, 43f);
    private List<GameObject> activeNPCs = new List<GameObject>();
    private List<GameObject> activeWaypoints = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void ClearExistingNPCs()
    {
        Debug.Log($"Clearing {activeNPCs.Count} existing NPCs");
        foreach (GameObject npc in activeNPCs)
        {
            if (npc != null)
            {
                Destroy(npc);
            }
        }
        activeNPCs.Clear();

        foreach (GameObject waypoint in activeWaypoints)
        {
            if (waypoint != null)
            {
                Destroy(waypoint);
            }
        }
        activeWaypoints.Clear();
    }

    public void SpawnNPCsForBuilding(Vector3 buildingPosition, int workerCount)
    {
        int npcCount = workerCount / 100;
        
        if (npcPrefab == null)
        {
            Debug.LogError("NPC Prefab is not assigned in the Inspector!");
            return;
        }
        
        GameObject waypoint = CreateWaypoint(buildingPosition);
        activeWaypoints.Add(waypoint);
        
        for (int i = 0; i < npcCount; i++)
        {
            GameObject npc = Instantiate(npcPrefab, defaultSpawnPosition, Quaternion.identity);
            activeNPCs.Add(npc);
            
            NPC_Moving npcMoving = npc.GetComponent<NPC_Moving>();
            if (npcMoving == null)
            {
                Debug.LogError("NPC_Moving component not found on spawned NPC!");
                continue;
            }
            
            npcMoving.destinations = new Transform[] { waypoint.transform };
            
            GameObject npcRef = npc;
            npcMoving.OnReachedDestination += () => {
                Debug.Log("OnReachedDestination triggered!");
                if (npcRef != null)
                {
                    activeNPCs.Remove(npcRef);
                    Destroy(npcRef);
                }
            };
        }
    }

    private GameObject CreateWaypoint(Vector3 position)
    {
        GameObject waypoint = new GameObject("Waypoint");
        waypoint.transform.position = position;
        return waypoint;
    }
}