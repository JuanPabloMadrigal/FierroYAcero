using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitialization : MonoBehaviour
{

    [SerializeField] private GameObject almacenMP;
    [SerializeField] private GameObject plantaCoque;
    [SerializeField] private GameObject horno1;
    [SerializeField] private GameObject aceracion;
    [SerializeField] private GameObject molinoCom;
    [SerializeField] private GameObject patioAce;


    public void BuildingsGeneration(GameModel gameData)
    {

        // Generación de edificios de proceso de acero

        Debug.Log(gameData.buildingsList[0].x);

        foreach(BuildingProperties edificio in gameData.buildingsList)
        {
            if (edificio.unlocked)
            {

                Debug.Log("Entra");

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
                    Debug.Log("Se intentó cargar una instancia de objeto no existente.");
                    break;
                }

                GameObject newGO = Instantiate(newBuilding, new Vector3(edificio.x, edificio.y, edificio.z), Quaternion.Euler(0, edificio.rot, 0));
            }
        }

        // Generación de almacén de materia prima

        if (gameData.ironStorehouse != null)
        {
            GameObject newGO = Instantiate(patioAce, new Vector3(gameData.ironStorehouse.x, gameData.ironStorehouse.y, gameData.ironStorehouse.z), Quaternion.Euler(0, gameData.ironStorehouse.rot, 0));
        }
        else
        {
            Debug.Log("Error al generar el almacén de MP");
        }

        // Generación de plante de coque

        if (gameData.cokePlant != null)
        {
            GameObject newGO = Instantiate(plantaCoque, new Vector3(gameData.cokePlant.x, gameData.cokePlant.y, gameData.cokePlant.z), Quaternion.Euler(0, gameData.cokePlant.rot, 0));
        }
        else
        {
            Debug.Log("Error al generar la planta de coque");
        }

        // Generación de patio de acero

        if (gameData.steelYard != null)
        {
            GameObject newGO = Instantiate(plantaCoque, new Vector3(gameData.steelYard.x, gameData.steelYard.y, gameData.steelYard.z), Quaternion.Euler(0, gameData.steelYard.rot, 0));
        }
        else
        {
            Debug.Log("Error al generar el patio de acero");
        }

    }
}
