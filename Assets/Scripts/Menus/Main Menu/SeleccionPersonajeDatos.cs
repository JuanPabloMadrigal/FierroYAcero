using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeleccionPersonajeDatos : MonoBehaviour
{

    public string modoJuego;
    private string eleccionFinal;
    public GameObject robotConfirmacion;
    public GameObject robotElegido;
    public AudioSource sonidoDeReloj;

    void Start()
    {

    }

    public IEnumerator elegirYComenzar()
    {

        if (eleccionFinal != string.Empty)
        {
            robotConfirmacion.SetActive(false);
            sonidoDeReloj.Play();
            robotElegido.SetActive(true);
            
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("StoryGame");
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

        StartCoroutine(elegirYComenzar());

    }

}
