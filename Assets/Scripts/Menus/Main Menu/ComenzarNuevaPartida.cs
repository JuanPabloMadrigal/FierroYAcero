using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;

public class ComenzarNuevaPartida : MonoBehaviour
{

    public string modoJuego;
    private string eleccionFinal;
    [SerializeField] private GameObject robotConfirmacion;
    [SerializeField] private GameObject robotElegido;
    [SerializeField] private GameObject videoPlayer;

    [SerializeField] private string filePath = "/Scripts/Story Mode/JSONs/";
    [SerializeField] AudioClip robotSFX;


    private string encKey; // Llave
    private string initVector; // IV

    void Start()
    {
        encKey = PlayerPrefs.GetString("EncKey");
        initVector = PlayerPrefs.GetString("InitVector");

        if (string.IsNullOrEmpty(encKey))
        {
            string newEncKey = CreateEncComponent(Random.Range(8, 13));
            encKey = newEncKey;
            PlayerPrefs.SetString("EncKey", newEncKey);
        }
        if (string.IsNullOrEmpty(initVector))
        {
            string newIV = CreateEncComponent(16);
            initVector = newIV;
            PlayerPrefs.SetString("InitVector", newIV);
        }
    }

    public IEnumerator elegirYComenzar()
    {

        if (eleccionFinal != string.Empty)
        {
            robotConfirmacion.SetActive(false);

            gameObject.GetComponent<AudioSource>().clip = robotSFX;
            gameObject.GetComponent<AudioSource>().time = 0.2f;
            gameObject.GetComponent<AudioSource>().Play();
            robotElegido.SetActive(true);
            
            yield return new WaitForSecondsRealtime(1.5f);
            gameObject.GetComponent<MenuNavegation>().GoToGame();
        }
        else
        {
            Debug.Log("Ningun personaje elegido");
        }
    }



    public void eleccionDePersonaje()
    {
        SeleccionPersonajeReto scriptSeleccionEduardo = GameObject.FindGameObjectWithTag("EduardoMenuSpriteArea").GetComponent<SeleccionPersonajeReto>();
        SeleccionPersonajeReto scriptSeleccionCarmina = GameObject.FindGameObjectWithTag("CarminaMenuSpriteArea").GetComponent<SeleccionPersonajeReto>();

        if (scriptSeleccionEduardo.personajeSeleccionado == true)
        {
            eleccionFinal = "Eduardo";
        }
        else
        if (scriptSeleccionCarmina.personajeSeleccionado == true)
        {
            eleccionFinal = "Carmina";
        }

        Debug.Log(eleccionFinal);

        createGameFile();
        StartCoroutine(elegirYComenzar());

    }

    public void createGameFile()
    {
        // Modelo de juego al iniciar desde cero
        GameModel gameData = new GameModel(
                    eleccionFinal, // Personaje
                    new List<BuildingProperties> // Edificios proceso de acero
                    {
                        new BuildingProperties(100, 0, 10f, 1f, 1000, true, "Horno 1", -19, -0.1f, -7, -90),
                        new BuildingProperties(100, 0, 15f, 1f, 0, false, "Horno 2", -15, -0.1f, -30, 90),
                        new BuildingProperties(100, 0, 15f, 1f, 0, false, "Horno 3", -8, -0.1f, 10, 0),
                        new BuildingProperties(100, 0, 2f, 1f, 1000, true, "Aceracion", -17, -0.05f, -15, 0),
                        new BuildingProperties(100, 0, 2f, 1f, 0, false, "Aceracion", -17, -0.05f, -21, 0),
                        new BuildingProperties(100, 0, 2f, 1f, 0, false, "Aceracion", 0, -0.05f, -17, 0),
                        new BuildingProperties(100, 0, 2f, 1f, 0, false, "Aceracion", 0, -0.05f, -23, 0),
                        new BuildingProperties(100, 0, 2f, 1f, 0, false, "Aceracion", 0, -0.05f, -29, 0),
                        new BuildingProperties(100, 0, 2f, 1f, 0, false, "Aceracion", 6, -0.05f, -23, 90),
                        new BuildingProperties(100, 0, 2f, 1f, 0, false, "Aceracion", 12, -0.05f, -21, 90),
                        new BuildingProperties(100, 0, 2f, 1f, 0, false, "Aceracion", 18, -0.05f, -21, 90),
                        new BuildingProperties(100, 0, 7f, 1f, 1000, true, "Molino Comercial", 19, -0.075f, 26, -90)
                    },
                    new IronStorehouse(100, 0, false, -16, 0.025f, 0, 180), // Almacen de MP
                    new CokePlant(100, 0, false, -22f, -0.025f, 0, 0), // Planta de coque
                    new SteelYard(100, 0, true, 19, -0.075f, 18, -90) // Patio de acero
                    );

        string targetSaveFile = PathManager.Instance.saveFileToUse;
        string fullPath = string.Empty;

        if (!PathManager.Instance.isLocal)
        {
            fullPath = Application.persistentDataPath + "/" + targetSaveFile;
        }
        else
        {
            fullPath = Application.dataPath + filePath + targetSaveFile;
        }

        bool fileValidation;
        fileValidation = File.Exists(fullPath);
        string json = CryptoHandler.EncryptStringAlt(JsonUtility.ToJson(gameData), encKey, initVector);

        if (!fileValidation)
        {
            Debug.Log(targetSaveFile);
            File.WriteAllBytes(fullPath, System.Text.Encoding.UTF8.GetBytes(json));
        }
        else
        {
            File.Delete(fullPath);
            File.WriteAllBytes(fullPath, System.Text.Encoding.UTF8.GetBytes(json));
        }

        RefreshEditorProjectWindow();
    }

    private string CreateEncComponent(int length)
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

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
