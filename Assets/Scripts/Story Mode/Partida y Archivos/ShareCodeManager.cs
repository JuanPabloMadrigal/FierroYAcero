using UnityEngine;
using System.Collections;
using System.Text;

[System.Serializable]
public class ApiResponse<T>
{
    public T data;
}

[System.Serializable]
public class ShareCodeData
{
    public string share_code;
}

public class ShareCodeManager : MonoBehaviour 
{
    [SerializeField] private PauseMenu pauseMenu;
    public static ShareCodeManager Instance;
    private HttpRequestHandler httpHandler;
    private string apiBaseUrl = "http://127.0.0.1:5000/api";

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
        httpHandler = new HttpRequestHandler();
    }

    public IEnumerator GenerateShareCode()
    {
        string saveData = JsonUtility.ToJson(FileHandlerStory.Instance.gameData);
        Debug.Log("Save Data to send: " + saveData);
        
        httpHandler.method = WebMethod.POST;
        httpHandler.payload = saveData;
        
        yield return StartCoroutine(httpHandler.ExecuteRequest($"{apiBaseUrl}/saves"));
        
        Debug.Log("API Response: " + httpHandler.result);
        
        if (!string.IsNullOrEmpty(httpHandler.result))
        {
            try 
            {
                ApiResponse<ShareCodeData> response = JsonUtility.FromJson<ApiResponse<ShareCodeData>>(httpHandler.result);
                if (response != null && response.data != null)
                {
                    Debug.Log($"Share Code Generated: {response.data.share_code}");
                    ShareCodeUI.Instance.DisplayShareCode(response.data.share_code);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error parsing response: {e.Message}");
                Debug.LogError($"Response content: {httpHandler.result}");
            }
        }
    }

    public IEnumerator LoadFromShareCode(string shareCode)
    {
        httpHandler.method = WebMethod.GET;
        
        yield return StartCoroutine(httpHandler.ExecuteRequest($"{apiBaseUrl}/saves/{shareCode}"));
        
        if (!string.IsNullOrEmpty(httpHandler.result))
        {
            ApiResponse<GameModel> response = JsonUtility.FromJson<ApiResponse<GameModel>>(httpHandler.result);
            if (response != null && response.data != null)
            {
                FileHandlerMultiplayer.Instance.gameData = response.data;
                
                yield return StartCoroutine(GameObject.FindGameObjectWithTag("GameMechanics")
                    .GetComponent<SceneInitialization>()
                    .RestartChildBuildings(FileHandlerMultiplayer.Instance.gameData));
                    
                UIManager.Instance.UpdateMoneyUI(response.data.money);
                UIManager.Instance.UpdateCoqueUI(response.data.coque);
                UIManager.Instance.UpdateIronUI(response.data.iron);

                pauseMenu.OnMultiplayerExit();
                pauseMenu.OnContinueButton();

            }
            else
            {
                Debug.LogError("Failed to parse save data from API");
            }
        }
    }
} 