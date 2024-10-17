using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TechNode
{
    public string name;
    public string description;
    public int cost;
    public bool isUnlocked;
    public List<string> prerequisites = new List<string>();
}

public class TechTree : MonoBehaviour
{
    public List<TechNode> technologies = new List<TechNode>();
    public int currentResearchPoints = 0;

    private Dictionary<string, TechNode> techDictionary = new Dictionary<string, TechNode>();

    // Event that is triggered when a technology is unlocked
    public event Action<string> OnTechUnlocked;

    void Start()
    {
        // Populate the dictionary for easy lookup
        foreach (var tech in technologies)
        {
            techDictionary[tech.name] = tech;
        }
    }

    public bool CanUnlockTech(string techName)
    {
        if (!techDictionary.ContainsKey(techName))
            return false;

        var tech = techDictionary[techName];

        if (tech.isUnlocked)
            return false;

        if (currentResearchPoints < tech.cost)
            return false;

        foreach (var prereq in tech.prerequisites)
        {
            if (!techDictionary.ContainsKey(prereq) || !techDictionary[prereq].isUnlocked)
                return false;
        }

        return true;
    }

    public bool UnlockTech(string techName)
    {
        if (!CanUnlockTech(techName))
            return false;

        var tech = techDictionary[techName];
        tech.isUnlocked = true;
        currentResearchPoints -= tech.cost;

        Debug.Log($"Unlocked technology: {tech.name}");

        // Trigger the event
        OnTechUnlocked?.Invoke(techName);

        return true;
    }

    public void AddResearchPoints(int points)
    {
        currentResearchPoints += points;
        Debug.Log($"Current research points: {currentResearchPoints}");
    }
}
