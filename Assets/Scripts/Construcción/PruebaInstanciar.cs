using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PruebaInstanciar : MonoBehaviour
{

    public GameObject Edificio_Horno1;
    public GameObject Edificio_AlmacenMP;

    private GameObject[] listaEdificios;

    private float posXHorno1;
    private float posXAlmacenMP;

    private bool xKeyPressed;
    private bool zKeyPressed;

    public void Awake()
    {
        posXHorno1 = 0f;
        posXAlmacenMP = 0f;
        listaEdificios = new GameObject[0];
    }

    public void OnXKey(InputAction.CallbackContext ctx)
    {
        xKeyPressed = ctx.started;
        Debug.Log(xKeyPressed);
        if (xKeyPressed)
        {
            Debug.Log("X Detectado");
            GameObject nuevo_Horno1 = Instantiate(Edificio_Horno1, new Vector3(posXHorno1, 0f, 4f), Quaternion.identity);
            listaEdificios.Append(nuevo_Horno1);
            posXHorno1 += 2f;
        }
    }

    public void OnZKey(InputAction.CallbackContext ctx)
    {
        zKeyPressed = ctx.started;
        if (zKeyPressed)
        {
            Debug.Log("Z Detectado");
            GameObject nuevo_AlmacenMP = Instantiate(Edificio_AlmacenMP, new Vector3(posXAlmacenMP, 0f, 8f), Quaternion.identity);
            listaEdificios.Append(nuevo_AlmacenMP);
            posXAlmacenMP += 2f;
        }
    }
}
