using UnityEngine;
using System.Collections.Generic;

public class TechTreeUI : MonoBehaviour
{
    [SerializeField] private TechTree techTree;
    [SerializeField] private GameObject techNodePrefab;
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject TechCanvas;
    
    [Header("Layout Settings")]
    [SerializeField] private float nodeSpacing = 150f;
    [SerializeField] private float levelSpacing = 200f;
    [SerializeField] private int margin1 = 10;
    [SerializeField] private int margin2 = 10;

    private Dictionary<string, TechNodeUI> nodeUIs = new Dictionary<string, TechNodeUI>();

    public void ShowTechTree()
    {
        if (techTree == null)
        {
            Debug.LogError("TechTree reference not set in TechTreeUI");
            return;
        }

        if (!techTree.IsInitialized())
        {
            techTree.InitializeDictionary();
            InitializeTechTree();
        }

        techTree.SetResearchPoints(150);  // Test with 150 points
        InitializeTechTree();
        TechCanvas.SetActive(true);
    }

    public void HideTechTree()
    {
        if (TechCanvas != null)
        {
            foreach (Transform child in contentParent)
            {
                if (child.gameObject.GetComponent<TechNodeUI>() != null)
                {
                    Destroy(child.gameObject);
                }
            }
            nodeUIs.Clear();
            TechCanvas.SetActive(false);
        }
        else
        {
            Debug.LogWarning("TechCanvas reference is null in TechTreeUI");
        }
    }

    private void InitializeTechTree()
    {
        // Clear any existing nodes
        foreach (Transform child in contentParent)
        {
            if (child.gameObject.GetType() == typeof(TechNodeUI))
            {
                Destroy(child.gameObject);
            } 
        }
        nodeUIs.Clear();

        // Create dictionaries to track tech columns and vertical positions
        Dictionary<string, int> techColumns = CalculateTechColumns();
        Dictionary<string, int> verticalLevels = CalculateVerticalLevels();

        // Create UI nodes
        foreach (var tech in techTree.technologies)
        {
            GameObject nodeObj = Instantiate(techNodePrefab, contentParent);
            TechNodeUI nodeUI = nodeObj.GetComponent<TechNodeUI>();

            // Position the node based on its column and level
            int column = techColumns[tech.name];
            int level = verticalLevels[tech.name];
            float xPos = column * levelSpacing;
            float yPos = -level * nodeSpacing;  // Negative to go downward

            // Adjust the RectTransform to anchor to the top-left
            RectTransform rectTransform = nodeObj.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1);  // Top-left
            rectTransform.anchorMax = new Vector2(0, 1);  // Top-left
            rectTransform.pivot = new Vector2(0, 1);      // Pivot at the top-left

            // Set the position, adjusting for node size (optional margin of 10 pixels from the edge)
            rectTransform.anchoredPosition = new Vector2(xPos + margin1, yPos - margin2);

            nodeUI.Initialize(tech, techTree);
            nodeUIs[tech.name] = nodeUI;
        }
    }

    private Dictionary<string, int> CalculateTechColumns()
    {
        Dictionary<string, int> columns = new Dictionary<string, int>();
        int currentColumn = 0;

        // First, place all root nodes (no prerequisites) in their own columns
        foreach (var tech in techTree.technologies)
        {
            if (tech.prerequisites == null || tech.prerequisites.Count == 0)
            {
                columns[tech.name] = currentColumn++;
            }
        }

        // Then, assign child nodes to the same column as their first prerequisite
        bool assigned;
        do
        {
            assigned = false;
            foreach (var tech in techTree.technologies)
            {
                if (columns.ContainsKey(tech.name))
                    continue;

                if (tech.prerequisites != null && tech.prerequisites.Count > 0)
                {
                    string firstPrereq = tech.prerequisites[0];
                    if (columns.ContainsKey(firstPrereq))
                    {
                        columns[tech.name] = columns[firstPrereq];
                        assigned = true;
                    }
                }
            }
        } while (assigned);

        return columns;
    }

    private Dictionary<string, int> CalculateVerticalLevels()
    {
        Dictionary<string, int> levels = new Dictionary<string, int>();

        // Root nodes start at level 0
        foreach (var tech in techTree.technologies)
        {
            if (tech.prerequisites == null || tech.prerequisites.Count == 0)
            {
                levels[tech.name] = 0;
            }
        }

        // Calculate levels for other nodes based on prerequisites
        bool changed;
        do
        {
            changed = false;
            foreach (var tech in techTree.technologies)
            {
                if (levels.ContainsKey(tech.name))
                    continue;

                if (tech.prerequisites != null && tech.prerequisites.Count > 0)
                {
                    bool allPrereqsAssigned = true;
                    int maxPrereqLevel = -1;

                    foreach (var prereq in tech.prerequisites)
                    {
                        if (!levels.ContainsKey(prereq))
                        {
                            allPrereqsAssigned = false;
                            break;
                        }
                        maxPrereqLevel = Mathf.Max(maxPrereqLevel, levels[prereq]);
                    }

                    if (allPrereqsAssigned)
                    {
                        levels[tech.name] = maxPrereqLevel + 1;
                        changed = true;
                    }
                }
            }
        } while (changed);

        return levels;
    }
}