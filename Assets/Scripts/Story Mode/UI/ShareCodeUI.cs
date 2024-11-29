using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShareCodeUI : MonoBehaviour
{
    public static ShareCodeUI Instance;
    [SerializeField] private TMP_InputField shareCodeInput;
    [SerializeField] private Button generateButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private TextMeshProUGUI shareCodeText;
    
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
    
    private void Start()
    {
        generateButton.onClick.AddListener(GenerateShareCode);
        loadButton.onClick.AddListener(LoadShareCode);
    }
    
    private void GenerateShareCode()
    {
        StartCoroutine(ShareCodeManager.Instance.GenerateShareCode());
    }
    
    private void LoadShareCode()
    {
        string code = shareCodeInput.text.Trim();
        if (!string.IsNullOrEmpty(code))
        {
            StartCoroutine(ShareCodeManager.Instance.LoadFromShareCode(code));
        }
    }

    public void DisplayShareCode(string code)
    {
        shareCodeText.text = $"Share Code: {code}";
    }
} 