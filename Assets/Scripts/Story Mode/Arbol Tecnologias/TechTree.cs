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
    private bool isInitialized = false;

    void Awake()
    {
        InitializeDictionary();
    }

    public void InitializeDictionary()
    {
        techDictionary.Clear();
        foreach (var tech in technologies)
        {
            Debug.Log($"Adding tech to dictionary: {tech.name}");
            techDictionary[tech.name] = tech;
        }
        isInitialized = true;
    }

    public bool IsInitialized()
    {
        return isInitialized;
    }

    // Remove the dictionary population from Start
    void Start()
    {
        if (!isInitialized)
        {
            InitializeDictionary();
        }
    }

    // Event that is triggered when a technology is unlocked
    public event Action<string> OnTechUnlocked;

    public bool CanUnlockTech(string techName)
    {
        if (!techDictionary.ContainsKey(techName))
        {
            Debug.Log($"{techName}: Not found in dictionary");
            return false;
        }

        var tech = techDictionary[techName];

        if (tech.isUnlocked)
        {
            Debug.Log($"{techName}: Already unlocked");
            return false;
        }

        Debug.Log($"{techName}: Cost {tech.cost}, Current Points {currentResearchPoints}");
        if (currentResearchPoints < tech.cost)
        {
            Debug.Log($"{techName}: Insufficient points");
            return false;
        }

        if (tech.prerequisites == null || tech.prerequisites.Count == 0)
        {
            Debug.Log($"{techName}: No prerequisites, available");
            return true;
        }

        foreach (var prereq in tech.prerequisites)
        {
            if (!techDictionary.ContainsKey(prereq) || !techDictionary[prereq].isUnlocked)
            {
                Debug.Log($"{techName}: Missing prerequisite {prereq}");
                return false;
            }
        }

        Debug.Log($"{techName}: All checks passed, available");
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

    public TechNode GetTechNode(string techName)
    {
        if (techDictionary.ContainsKey(techName))
        {
            return techDictionary[techName];
        }
        return null;
    }

    public void SetResearchPoints(int points)
    {
        currentResearchPoints = points;
        Debug.Log($"Research points set to: {currentResearchPoints}");
    }
}
