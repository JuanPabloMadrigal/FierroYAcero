using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TechNodeUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button unlockButton;
    [SerializeField] private Button showDescriptionButton;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private GameObject descriptionPanel;
    
    [Header("Colors")]
    [SerializeField] private Color unlockedColor = Color.green;
    [SerializeField] private Color availableColor = Color.white;
    [SerializeField] private Color lockedColor = Color.gray;

    private TechTree techTree;
    private string techName;
    private bool isExpanded = false;

    public void Initialize(TechNode node, TechTree tree)
    {
        // Validate input parameters
        if (node == null)
        {
            Debug.LogError("TechNodeUI: Received null TechNode");
            return;
        }
        if (tree == null)
        {
            Debug.LogError("TechNodeUI: Received null TechTree");
            return;
        }

        // Validate required components
        if (nameText == null) Debug.LogError("TechNodeUI: nameText is not assigned");
        if (costText == null) Debug.LogError("TechNodeUI: costText is not assigned");
        if (descriptionText == null) Debug.LogError("TechNodeUI: descriptionText is not assigned");
        if (unlockButton == null) Debug.LogError("TechNodeUI: unlockButton is not assigned");
        if (backgroundImage == null) Debug.LogError("TechNodeUI: backgroundImage is not assigned");
        if (descriptionPanel == null) Debug.LogError("TechNodeUI: descriptionPanel is not assigned");

        techTree = tree;
        techName = node.name;

        // Only set text if the components exist
        if (nameText != null) nameText.text = node.name;
        if (costText != null) costText.text = $"Cost: {node.cost}";
        if (descriptionText != null) descriptionText.text = node.description;
        
        // Only set description panel if it exists
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(false);
        }

        UpdateVisuals(node);

        // Only add listeners if the components exist
        if (unlockButton != null)
        {
            unlockButton.onClick.AddListener(OnUnlockClicked);
        }

        if (showDescriptionButton != null)
        {
            showDescriptionButton.onClick.AddListener(ToggleDescription);
        }
        else
        {
            Debug.LogError("TechNodeUI: No Button component found on the GameObject");
        }
        
        techTree.OnTechUnlocked += OnAnyTechUnlocked;
    }

    private void ToggleDescription()
    {
        isExpanded = !isExpanded;
        descriptionPanel.SetActive(isExpanded);
    }

    private void OnUnlockClicked()
    {
        if (techTree.UnlockTech(techName))
        {
            UpdateVisuals(techTree.GetTechNode(techName));
        }
    }

    private void OnAnyTechUnlocked(string unlockedTechName)
    {
        UpdateVisuals(techTree.GetTechNode(techName));
    }

    private void UpdateVisuals(TechNode node)
    {
        if (node.isUnlocked)
        {
            backgroundImage.color = unlockedColor;
            unlockButton.interactable = false;
            Debug.Log($"{node.name}: Already unlocked");
        }
        else if (techTree.CanUnlockTech(techName))
        {
            backgroundImage.color = availableColor;
            unlockButton.interactable = true;
            Debug.Log($"{node.name}: Available to unlock");
        }
        else
        {
            backgroundImage.color = lockedColor;
            unlockButton.interactable = false;
            Debug.Log($"{node.name}: Locked - Prerequisites not met or insufficient points");
        }
    }

    private void OnDestroy()
    {
        if (techTree != null)
        {
            techTree.OnTechUnlocked -= OnAnyTechUnlocked;
        }
    }
} 