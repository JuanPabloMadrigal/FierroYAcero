using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeleccionPersonajeReto : MonoBehaviour
{

    public GameObject personajeElegido;
    public GameObject personajeSinElegir;
    public GameObject robotNeutral;
    public GameObject robotConfirmacion;
    public GameObject robot;
    public GameObject elegirTexto;
    public GameObject confirmacionTexto;
    float escalaXOriginal;
    float escalaYOriginal;
    Vector3 velocidadEscala;
    public bool personajeSeleccionado;

    private void Start()
    {
        escalaXOriginal = transform.localScale.x;
        escalaYOriginal = transform.localScale.y;
        personajeSeleccionado = false;
        velocidadEscala = Vector3.zero;
    }

    public void PersonajeHover(){
        if (!personajeSeleccionado)
        {
            personajeElegido.SetActive(true);
            personajeSinElegir.SetActive(false);
            StartCoroutine(EscalarEnHover());
        }
    }
    private IEnumerator EscalarEnHover()
    {
        Debug.Log("Entra al escalar en hover");
        Vector3 escalaInicial = transform.localScale;
        Vector3 escalaFinal = new Vector3(escalaXOriginal,escalaYOriginal, 1) * 1.05f;
        float tiempo = 0f;

            while (tiempo < 0.1f)
            {
                float t = tiempo / 0.1f;
                Debug.Log(t);
            transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, t);
                tiempo += Time.deltaTime;
                yield return null;
            }

        transform.localScale = escalaFinal;
    }

    public void PersonajeFueraHover()
    {
        if (!personajeSeleccionado)
        {
            personajeElegido.SetActive(false);
            personajeSinElegir.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(EscalarFueraHover());
        }
    }

    public void PersonajeSeleccion()
    {
        if (!personajeSeleccionado)
        {
            personajeElegido.SetActive(true);
            personajeSinElegir.SetActive(false);
            StopAllCoroutines();
            personajeSeleccionado = true;
            if (gameObject.tag == "CarminaMenuSpriteArea")
            {
                SeleccionPersonajeReto eduardo = GameObject.FindGameObjectWithTag("EduardoMenuSpriteArea").GetComponent<SeleccionPersonajeReto>();
                eduardo.personajeSeleccionado = false;
                eduardo.PersonajeFueraHover();
            }
            else
            {
                SeleccionPersonajeReto carmina = GameObject.FindGameObjectWithTag("CarminaMenuSpriteArea").GetComponent<SeleccionPersonajeReto>();
                carmina.personajeSeleccionado = false;
                carmina.PersonajeFueraHover();
            }
            elegirTexto.SetActive(false);
            confirmacionTexto.SetActive(true);
            robotNeutral.SetActive(false);
            robotConfirmacion.SetActive(true);
            robot.GetComponent<Button>().enabled = true;
        }
    }

    private IEnumerator EscalarFueraHover()
    {
        Vector2 escalaFinal = new Vector3(escalaXOriginal, escalaYOriginal, 1);
        float tiempo = 0.1f;

        while (tiempo > 0)
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, escalaFinal, ref velocidadEscala, 0.05f);
            tiempo -= Time.deltaTime;
            yield return null;
        }

        transform.localScale = escalaFinal;
    }

}
