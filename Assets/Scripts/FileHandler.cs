using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using UnityEditor;
using UnityEngine;

public class FileHandler : MonoBehaviour
{
    /// <summary>
    /// Manejo de clase como singleton
    /// </summary>
    public static FileHandler Instance;


    /// <summary>
    /// variable local para determinar si la lectura de archivo
    /// sera local en el editor o sera en directorio persistente
    /// </summary>
    [SerializeField] private bool isLocal = true;
    [SerializeField] private bool fileValidation;
    [SerializeField] private string JSONFile = "players.txt";
    [SerializeField] private string filePath = "/Scripts/";
    public List<GameObject> NewplayerObj;
    public List<BuildingProperties> buildingData;


    private void Awake()
    {
        Instance = this;
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
            Debug.Log(streamContent);
            string decode = CryptoHandler.DecryptStringAlt(streamContent, "JKAID99OA1");
            buildingData =  JsonUtility.FromJson<List<BuildingProperties>>(decode);
            for(int NewObj = 0; NewObj < NewplayerObj.Count; NewObj++)
            {

                NewplayerObj[NewObj].GetComponent<BuildingProperties>().costPerTurn = buildingData[NewObj].costPerTurn;
                NewplayerObj[NewObj].GetComponent<BuildingProperties>().costToBuild = buildingData[NewObj].costToBuild;
                NewplayerObj[NewObj].GetComponent<BuildingProperties>().profitPerTurn = buildingData[NewObj].profitPerTurn;
            }
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
        for(int NewObj = 0; NewObj < NewplayerObj.Count; NewObj++)
        {

            buildingData[NewObj].costPerTurn = NewplayerObj[NewObj].GetComponent<BuildingProperties>().costPerTurn;
            buildingData[NewObj].costToBuild = NewplayerObj[NewObj].GetComponent<BuildingProperties>().costToBuild;
            buildingData[NewObj].profitPerTurn = NewplayerObj[NewObj].GetComponent<BuildingProperties>().profitPerTurn;
        }
        string json = CryptoHandler.EncryptStringAlt(JsonUtility.ToJson(buildingData), "JKAID99OA1");
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
        RefreshEditorProjectWindow();

    }

    void RefreshEditorProjectWindow()
    {
        #if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
        #endif
    }

}
