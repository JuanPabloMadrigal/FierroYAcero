using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechEffects : MonoBehaviour
{
    public TechTree techTree;

    void Start()
    {
        // Subscribe to the OnTechUnlocked event
        techTree.OnTechUnlocked += HandleTechUnlocked;
    }

    void HandleTechUnlocked(string techName)
    {
        switch (techName)
        {
            case "Research":
                ImproveResearchSpeed(15);
                break;
            // Add more cases for other technologies
            default:
                Debug.Log($"Technology unlocked: {techName}");
                break;
        }
    }

    void ImproveResearchSpeed(int percentage)
    {
        Debug.Log($"Research speed improved by {percentage}%");
        // Implement the research speed improvement
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed
        if (techTree != null)
        {
            techTree.OnTechUnlocked -= HandleTechUnlocked;
        }
    }
}
