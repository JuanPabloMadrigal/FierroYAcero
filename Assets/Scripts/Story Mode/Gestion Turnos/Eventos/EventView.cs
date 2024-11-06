using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventView : MonoBehaviour
{
    [SerializeField] private GameObject eventScreen;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;

    public void ShowEventScreen(EventModel newEvent){
        eventScreen.SetActive(true);
        title.text = newEvent.name;
        description.text = newEvent.description;
    }

    public void CloseEventScreen()
    {
        eventScreen.SetActive(false);
    }

}
