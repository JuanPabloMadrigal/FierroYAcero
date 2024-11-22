using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneInitialization : MonoBehaviour
{

    [SerializeField] private GameObject almacenMP;
    [SerializeField] private GameObject plantaCoque;
    [SerializeField] private GameObject horno1;
    [SerializeField] private GameObject aceracion;
    [SerializeField] private GameObject molinoCom;
    [SerializeField] private GameObject patioAce;
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

                GameObject newGO = Instantiate(newBuilding, new Vector3(edificio.x, edificio.y, edificio.z), Quaternion.Euler(0, edificio.rot, 0));
                if (edificio.type == "Horno 1")
                {
                    newGO.GetComponent<Button>().onClick.AddListener(delegate { sidebarControl.OpenSideBar("horno1"); } );
                }
            }
        }

        // Generaci�n de almac�n de materia prima

        if (!ReferenceEquals(gameData.ironStorehouse, null))
        {
            GameObject newGO = Instantiate(almacenMP, new Vector3(gameData.ironStorehouse.x, gameData.ironStorehouse.y, gameData.ironStorehouse.z), Quaternion.Euler(0, gameData.ironStorehouse.rot, 0));
            newGO.GetComponent<Button>().onClick.AddListener(delegate { sidebarControl.OpenSideBar("hierro"); });
        }
        else
        {
            Debug.Log("Error al generar el almac�n de MP");
        }

        // Generaci�n de plante de coque

        if (!ReferenceEquals(gameData.cokePlant, null))
        {
            GameObject newGO = Instantiate(plantaCoque, new Vector3(gameData.cokePlant.x, gameData.cokePlant.y, gameData.cokePlant.z), Quaternion.Euler(0, gameData.cokePlant.rot, 0));
            newGO.GetComponent<Button>().onClick.AddListener(delegate { sidebarControl.OpenSideBar("plantadecoque"); });
        }
        else
        {
            Debug.Log("Error al generar la planta de coque");
        }

        // Generaci�n de patio de acero

        if (!ReferenceEquals(gameData.steelYard, null))
        {
            GameObject newGO = Instantiate(patioAce, new Vector3(gameData.steelYard.x, gameData.steelYard.y, gameData.steelYard.z), Quaternion.Euler(0, gameData.steelYard.rot, 0));
        }
        else
        {
            Debug.Log("Error al generar el patio de acero");
        }

    }
}
