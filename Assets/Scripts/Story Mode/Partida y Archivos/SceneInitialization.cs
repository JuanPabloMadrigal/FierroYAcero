using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneInitialization : MonoBehaviour
{

    [SerializeField] private GameObject almacenMP;
    [SerializeField] private GameObject plantaCoque;
    [SerializeField] private GameObject horno1;
    [SerializeField] private GameObject horno2;
    [SerializeField] private GameObject horno3;
    [SerializeField] private GameObject aceracion;
    [SerializeField] private GameObject molinoCom;
    [SerializeField] private GameObject patioAce;

    [SerializeField] private GameObject padreEdificios;

    [SerializeField] private SidebarControl sidebarControl;
    public Animator sceneAnimator;
    [SerializeField]private GameObject loadScreen;
    [SerializeField]private RawImage loadScreenImage;
    

    void Start()
    {
        loadScreenImage = loadScreen.GetComponent<RawImage>();
        loadScreenImage.color = new Color(0, 0, 0, 1);
        sceneAnimator.Play("FadeIn");
        StartCoroutine("FadeIn");
    }

    public IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1.0f);
        loadScreen.SetActive(false);
    }

    public void BuildingsGeneration(GameModel gameData)
    {

        // Generaci�n de edificios de proceso de acero

        foreach(BuildingProperties edificio in gameData.buildingsList)
        {
            if (edificio.unlocked)
            {

                GameObject newBuilding;
                
                if (edificio.type == "Horno 1")
                {
                    newBuilding = horno1;
                }
                else if (edificio.type == "Horno 2")
                {
                    newBuilding = horno2;
                }
                else if (edificio.type == "Horno 3")
                {
                    newBuilding = horno3;
                }
                else if (edificio.type == "Aceracion")
                {
                    newBuilding = aceracion;
                }
                else if (edificio.type == "Molino Comercial")
                {
                    newBuilding = molinoCom;
                }
                else if (edificio.type == "Planta Acero")
                {
                    newBuilding = patioAce;
                }
                else
                {
                    Debug.Log("Se intent� cargar una instancia de objeto no existente.");
                    break;
                }

                GameObject newGO = Instantiate(newBuilding, new Vector3(edificio.x, edificio.y, edificio.z), Quaternion.Euler(0, edificio.rot, 0), padreEdificios.transform);
                if (edificio.type == "Horno 1")
                {
                    try
                    {
                        newGO.GetComponent<Button>().onClick.AddListener(delegate { sidebarControl.OpenSideBar("horno1"); });
                    }
                    catch { }
                }

                if (edificio.type == "Aceracion")
                {
                    try
                    {
                        newGO.GetComponent<Button>().onClick.AddListener(delegate { sidebarControl.OpenSideBar("aceracion"); });
                    }
                    catch { }
                }
            }
        }

        // Generaci�n de almac�n de materia prima

        if (!ReferenceEquals(gameData.ironStorehouse, null) && gameData.ironStorehouse.unlocked)
        {
            GameObject newGO = Instantiate(almacenMP, new Vector3(gameData.ironStorehouse.x, gameData.ironStorehouse.y, gameData.ironStorehouse.z), Quaternion.Euler(0, gameData.ironStorehouse.rot, 0), padreEdificios.transform);
            try
            {
                newGO.GetComponent<Button>().onClick.AddListener(delegate { sidebarControl.OpenSideBar("hierro"); });
            }
            catch { }
        }
        else
        {
            Debug.Log("El jugador no tiene el almacén de MP desbloqueado o no se puede cargar");
        }

        // Generaci�n de plante de coque

        if (!ReferenceEquals(gameData.cokePlant, null) && gameData.cokePlant.unlocked)
        {
            GameObject newGO = Instantiate(plantaCoque, new Vector3(gameData.cokePlant.x, gameData.cokePlant.y, gameData.cokePlant.z), Quaternion.Euler(0, gameData.cokePlant.rot, 0), padreEdificios.transform);
            try
            {
                newGO.GetComponent<Button>().onClick.AddListener(delegate { sidebarControl.OpenSideBar("plantadecoque"); });
            }
            catch { }
        }
        else
        {
            Debug.Log("El jugador no tiene la planta de coque desbloqueada o no se puede cargar");
        }

        // Generaci�n de patio de acero

        if (!ReferenceEquals(gameData.steelYard, null) && gameData.steelYard.unlocked)
        {
            GameObject newGO = Instantiate(patioAce, new Vector3(gameData.steelYard.x, gameData.steelYard.y, gameData.steelYard.z), Quaternion.Euler(0, gameData.steelYard.rot, 0), padreEdificios.transform);
            try
            {
                newGO.GetComponent<Button>().onClick.AddListener(delegate { sidebarControl.OpenSideBar("patio"); });
            }
            catch { }
        }
        else
        {
            Debug.Log("Error al generar el patio de acero");
        }

    }

    public IEnumerator RestartChildBuildings(GameModel gameModel)
    {
        Debug.Log($"Empieza reinicio");
        while (padreEdificios.transform.childCount > 0)
        {
            Destroy(padreEdificios.transform.GetChild(0).gameObject);
            Debug.Log($"Destruye uno");
            yield return null;
        }
        BuildingsGeneration(gameModel);
        foreach(BuildingProperties ace in gameModel.buildingsList)
        {
            if (ace.type == "Aceracion")
            {
                Debug.Log($"Taller de aceracion nuevo esta en: {ace.unlocked}");
            }
        }
    }

}
