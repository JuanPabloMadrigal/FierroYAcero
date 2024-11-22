using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficesOpenDialogue : MonoBehaviour
{

    [SerializeField] private GameObject indicator;

    public void OpenNewDialogue()
    {   
        if (indicator.activeInHierarchy)
        {
            FileHandlerStory.Instance.gameData.evento++;
            indicator.SetActive(false);
            Debug.Log($"Ahora el evento después del clic en la oficina es: {FileHandlerStory.Instance.gameData.evento}");
        }
    }
}
