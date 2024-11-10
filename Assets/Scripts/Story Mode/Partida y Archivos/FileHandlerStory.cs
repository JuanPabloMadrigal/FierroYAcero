using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private bool fileValidationDialogues;
    [SerializeField] private string EncJSONFile = "storyGame.txt";
    [SerializeField] private string JSONFile = "storyGame.txt";
    [SerializeField] private string DialogueJSONFile = "JSON Templates/storyDialoguesOriginal.txt";
    [SerializeField] private string DialogueEncJSONFile = "storyDialogues.txt";
    [SerializeField] private string filePath = "/Scripts/Story Mode/JSONs/";
    private string encKey; // Llave
    private string initVector; // IV

    // Depuración texto

    public Text textPartEnc;
    public Text textPartNorm;

    //

    // VARIABLE QUE GUARDA EL MODELO DE JUEGO DE LA PARTIDA

    public GameModel gameData;

    // VARIABLE QUE     CARGA LOS DIÁLOGOS DEL JUEGO

    public GameDialogues gameDialogues;

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

        // Carga de diálogos
        gameDialogues = new GameDialogues();

        // Obtención de datos para encriptación

        encKey = PlayerPrefs.GetString("EncKey");
        initVector = PlayerPrefs.GetString("InitVector");

        // Intento de carga de partida guardada
        ReadFile();

        // Se genera el mapa
        GameObject.FindGameObjectWithTag("GameMechanics").GetComponent<SceneInitialization>().BuildingsGeneration(gameData);

        // Se actualiza el UI en base a datos del modelo de juego
        UIManager.Instance.UpdateMoneyUI(gameData.money);
        UIManager.Instance.UpdateCoqueUI(gameData.coque);
    }


    // Lectura de archivos de progreso de juego y diálogos
    public void ReadFile()
    {
        string fullPath = string.Empty;
        string dialoguePath = string.Empty;

        if (!isLocal)
        {
            fullPath = Application.persistentDataPath + "/" + EncJSONFile;
            dialoguePath = Application.persistentDataPath + "/" + DialogueEncJSONFile;
        }
        else
        {
            fullPath = Application.dataPath + filePath + EncJSONFile;
            dialoguePath = Application.dataPath + filePath + DialogueEncJSONFile;
        }

        fileValidation = File.Exists(fullPath);
        if (!fileValidation)
        {
            Debug.Log("Game file not found");
        }
        else
        {

            string streamContent = "";
            StreamReader fileReader = new StreamReader(fullPath, Encoding.Default);
            streamContent = fileReader.ReadToEnd();
            fileReader.Close();
            string decode = CryptoHandler.DecryptStringAlt(streamContent, encKey, initVector);
            gameData =  JsonUtility.FromJson<GameModel>(decode);
            Debug.Log(decode);
            textPartNorm.text = "JSON Partida Normal: " + decode;
        }

        fileValidationDialogues = File.Exists(dialoguePath);
        if (!fileValidationDialogues)
        {
            Debug.Log("Dialogues file not found, encripted version weill be created.");
            EncryptExternalFile(DialogueJSONFile, DialogueEncJSONFile);
        }
            
        string streamDialogueContent = "";
        StreamReader fileReaderDialogue = new StreamReader(dialoguePath, Encoding.Default);
        streamDialogueContent = fileReaderDialogue.ReadToEnd();
        fileReaderDialogue.Close();
        string decodeDialogue = CryptoHandler.DecryptStringAlt(streamDialogueContent, encKey, initVector);
        gameDialogues = JsonUtility.FromJson<GameDialogues>(decodeDialogue);
        Debug.Log(decodeDialogue);
        Debug.Log(gameDialogues.Eventos[0].Dialogos[0].DialogoTexto);

        RefreshEditorProjectWindow();

    }

    // Guardado de nuevos datos del progreso del juego en el archivo original
    public void WriteFile()
    {
        string fullPath = string.Empty;

        if (!isLocal)
        {
            fullPath = Application.persistentDataPath + "/" + EncJSONFile;
        }
        else
        {
            fullPath = Application.dataPath + filePath + EncJSONFile;
        }

        fileValidation = File.Exists(fullPath);
        string json = CryptoHandler.EncryptStringAlt(JsonUtility.ToJson(gameData), encKey, initVector);

        if (!fileValidation)
        {
            textPartEnc.text = "Configuración en 'local''";
            Debug.Log(EncJSONFile);
            File.WriteAllBytes(fullPath, System.Text.Encoding.UTF8.GetBytes(json));
        }
        else
        {
            File.Delete(fullPath);
            File.WriteAllBytes(fullPath, System.Text.Encoding.UTF8.GetBytes(json));
        }
        Debug.Log(json);
        textPartEnc.text = "JSON Partida Enc: " + json;

        RefreshEditorProjectWindow();

    }


    // Función local para encriptar datos necesarios para el desarrollo del juego
    public void EncryptExternalFile(string basePath, string file)
    {
        string extPath = string.Empty;
        string originPath = string.Empty;

        if (!isLocal)
        {
            extPath = Application.persistentDataPath + "/" + file;
            originPath = Application.streamingAssetsPath + "/" + basePath;
        }
        else
        {
            extPath = Application.dataPath + filePath + file;
            originPath = Application.streamingAssetsPath + "/" + basePath;
        }


        ////// ERROR DE CARGAR DIALOGOS //////

        fileValidation = File.Exists(originPath);
        if (!fileValidation)
        {
            Debug.LogError("Archivo de origen no encontrado: " + originPath);
            return;
        }

        ////// ERROR DE CARGAR DIALOGOS //////

        string streamContent = "";
        StreamReader fileReader = new StreamReader(originPath, Encoding.Default);
        streamContent = fileReader.ReadToEnd();
        fileReader.Close();
        string json = CryptoHandler.EncryptStringAlt(streamContent, encKey, initVector);

        fileValidation = File.Exists(extPath);
        if (fileValidation)
        {
            File.Delete(extPath);
        }
        File.WriteAllBytes(extPath, Encoding.UTF8.GetBytes(json));

        RefreshEditorProjectWindow();

    }

    // Función para crear una llave de encriptación aleatoria
    /*private string CreateEncComponent(int length)
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        StringBuilder result = new StringBuilder(length);
        System.Random random = new System.Random();
        for (int i = 0; i < length; i++)
        {
            result.Append(characters[random.Next(characters.Length)]);
        }
        return result.ToString();
    }*/

    void RefreshEditorProjectWindow()
    {
        #if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
        #endif
    }

}
