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
    [SerializeField] private bool fileValidationDialogues;
    [SerializeField] private string JSONFile = "storyGame.txt";
    [SerializeField] private string DialogueJSONFile = "storyDialogues.txt";
    [SerializeField] private string filePath = "/Scripts/StoryMode/";
    private string encKey; // Llave
    private string initVector; // IV

    // VARIABLE QUE GUARDA EL MODELO DE JUEGO DE LA PARTIDA

    public GameModel gameData;

    // VARIABLE QUE CARGA LOS DIÁLOGOS DEL JUEGO

    public GameDialogues gameDialogues;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Definicion de modelo de juego vacío para intentar cargarlo en caso de existir una partida guardada
        gameData = new GameModel(
                    "", // Personaje
                    0, // Dinero
                    0, // Satisfaccion
                    0, // coque
                    20, // precio coque
                    0, // hierro
                    20, // precio hierro
                    new List<BuildingProperties> // Edificios proceso de acero
                    {
                        new BuildingProperties(0, 0, 10f, 1f, 0, false, "", 0, 0, 0, -90),
                        new BuildingProperties(0, 0, 20f, 1f, 0, false, "", 0, 0, 0, -90),
                        new BuildingProperties(0, 0, 30f, 1f, 0, false, "", 0, 0, 0, 0)
                    });

        // Carga de diálogos
        gameDialogues = new GameDialogues();

        // Obtención de datos para encriptación
        encKey = PlayerPrefs.GetString("EncKey");
        initVector = PlayerPrefs.GetString("InitVector");

        if (string.IsNullOrEmpty(encKey))
        {
            string newEncKey = CreateEncComponent(Random.Range(8, 13));
            PlayerPrefs.SetString("EncKey", newEncKey);
        }
        if (string.IsNullOrEmpty(initVector))
        {
            string newIV = CreateEncComponent(16);
            PlayerPrefs.SetString("InitVector", newIV);
        }

        // Intento de partida guardada
        ReadFile();

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
            fullPath = Application.persistentDataPath + "/" + JSONFile;
            dialoguePath = Application.persistentDataPath + "/" + DialogueJSONFile;
        }
        else
        {
            fullPath = filePath + JSONFile;
            dialoguePath = filePath + DialogueJSONFile;
        }

        fileValidation = File.Exists(Application.dataPath + fullPath);
        if (!fileValidation)
        {
            Debug.Log("Game file not found");
        }
        else
        {

            string streamContent = "";
            StreamReader fileReader = new StreamReader(Application.dataPath + fullPath, Encoding.Default);
            streamContent = fileReader.ReadToEnd();
            fileReader.Close();
            string decode = CryptoHandler.DecryptStringAlt(streamContent, encKey, initVector);
            gameData = JsonUtility.FromJson<GameModel>(decode);
            Debug.Log(decode);
        }

        fileValidationDialogues = File.Exists(Application.dataPath + dialoguePath);
        if (!fileValidationDialogues)
        {
            Debug.Log("Dialogues file not found");
        }
        else
        {
            string streamDialogueContent = "";
            StreamReader fileReaderDialogue = new StreamReader(Application.dataPath + dialoguePath, Encoding.Default);
            streamDialogueContent = fileReaderDialogue.ReadToEnd();
            fileReaderDialogue.Close();
            string decodeDialogue = CryptoHandler.DecryptStringAlt(streamDialogueContent, encKey, initVector);
            gameDialogues = JsonUtility.FromJson<GameDialogues>(decodeDialogue);
            Debug.Log(decodeDialogue);
            Debug.Log(gameDialogues.Eventos[0].Dialogos[0].DialogoTexto);
        }

        RefreshEditorProjectWindow();

    }

    // Guardado de nuevos datos del progreso del juego en el archivo original
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
        string json = CryptoHandler.EncryptStringAlt(JsonUtility.ToJson(gameData), encKey, initVector);
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


    // Función local para encriptar datos necesarios para el desarrollo del juego
    public void EncryptExternalFile(string content, string file)
    {
        string extPath = string.Empty;
        if (!isLocal)
        {
            extPath = Application.persistentDataPath + "/" + file;
        }
        else
        {
            extPath = filePath + file;
        }

        fileValidation = File.Exists(Application.dataPath + file);
        string json = CryptoHandler.EncryptStringAlt(content, encKey, initVector);
        Debug.Log(json);

        if (!fileValidation)
        {
            Debug.Log(file);
            File.WriteAllBytes(Application.dataPath + file, System.Text.Encoding.UTF8.GetBytes(json));
        }
        else
        {
            File.Delete(Application.dataPath + extPath);
            File.WriteAllBytes(Application.dataPath + extPath, System.Text.Encoding.UTF8.GetBytes(json));
        }

        RefreshEditorProjectWindow();

    }

    // Función para crear una llave de encriptación aleatoria
    private string CreateEncComponent(int length)
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        StringBuilder result = new StringBuilder(length);
        System.Random random = new System.Random();
        for (int i = 0; i < length; i++)
        {
            result.Append(characters[random.Next(characters.Length)]);
        }
        return result.ToString();
    }

    void RefreshEditorProjectWindow()
    {
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

}
