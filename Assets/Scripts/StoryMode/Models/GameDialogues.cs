using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDialogues
{
    
    public List<Evento> Eventos;

    public Evento ObtenerEvento(int indice)
    {
        if (indice >= 0 && indice < Eventos.Count)
        {
            return Eventos[indice];
        }
        else
        {
            Debug.Log("No se encuentran más dialogos para mostrar.");
            return null;
        }
    }
}

[System.Serializable]
public class Evento
{
    public List<Dialogo> Dialogos;
}

[System.Serializable]
public class Dialogo
{
    public string img_der;
    public string img_izq;
    public string DialogoTexto;
}
