using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using UnityEditor;
using UnityEngine;

public class FileHandlerStory : MonoBehaviour
{
    /// <summary>
    /// Manejo de clase como singleton
    /// </summary>
    public static FileHandlerStory Instance;


    /// <summary>
    /// variable local para determinar si la lectura de archivo
    /// sera local en el editor o sera en directorio persistente
    /// </summary>
    [SerializeField] private bool isLocal = true;
    [SerializeField] private bool fileValidation;
    [SerializeField] private string JSONFile = "game.txt";
    [SerializeField] private string filePath = "/Scripts/";

    // VARIABLE QUE GUARDA EL MODELO DE JUEGO DE LA PARTIDA

    public GameModel gameData;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameData = new GameModel(
                    "",
                    0,
                    0,
                    new List<BuildingProperties>
                    {
                        new BuildingProperties(0, 0, 10f, 1f, 0, false, "", 0, 0, 0, -90),
                        new BuildingProperties(0, 0, 20f, 1f, 0, false, "", 0, 0, 0, -90),
                        new BuildingProperties(0, 0, 30f, 1f, 0, false, "", 0, 0, 0, 0)
                    });
        ReadFile();
        Debug.Log(gameData.buildingsList[0].valueModifier);
        UIManager.Instance.UpdateMoneyUI(gameData.money); ;
    }

    public void ReadFile()
    {
        string fullPath = string.Empty;
        if (!isLocal)
        {
            fullPath = Application.persistentDataPath + "/" + JSONFile;
        }
        else
        {
            fullPath = filePath + JSONFile; 
        }

        fileValidation = File.Exists(Application.dataPath + fullPath);
        if (!fileValidation)
        {
            Debug.Log("file not found");
        }
        else
        {

            string streamContent = "";
            StreamReader fileReader = new StreamReader(Application.dataPath + fullPath, Encoding.Default);
            streamContent = fileReader.ReadToEnd();
            fileReader.Close();
            string decode = CryptoHandler.DecryptStringAlt(streamContent, "JKAID99OA1");
            gameData =  JsonUtility.FromJson<GameModel>(decode);
            Debug.Log(decode);
        }

        RefreshEditorProjectWindow();

    }

    public void WriteFile()
    {
        string fullPath = string.Empty;
        if (!isLocal)
        {
            fullPath = Application.persistentDataPath + "/" + JSONFile;
        }
        else
        {
            fullPath = filePath + JSONFile;
        }

        fileValidation = File.Exists(Application.dataPath + fullPath);
        string json = CryptoHandler.EncryptStringAlt(JsonUtility.ToJson(gameData), "JKAID99OA1");
        if (!fileValidation)
        {            
            Debug.Log(JSONFile);
            File.WriteAllBytes(Application.dataPath + fullPath, System.Text.Encoding.UTF8.GetBytes(json));
        }
        else
        {
            File.Delete(Application.dataPath + fullPath);            
            File.WriteAllBytes(Application.dataPath + fullPath, System.Text.Encoding.UTF8.GetBytes(json)); 
        }
        Debug.Log(json);
        RefreshEditorProjectWindow();

    }

    void RefreshEditorProjectWindow()
    {
        #if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
        #endif
    }

}
