using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EventRandomizer : MonoBehaviour
{
    public EventModel[] events;
    public int eventAmmount;
    // Start is called before the first frame update
    void Start()
    {
        EventSummon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void EventSummon()
    {
        int numberOfEvents = events.Length;
        List<EventModel> eventArray = new List<EventModel>(eventAmmount);
        for(int i = 0; i < eventAmmount; i++)
        {
            eventArray[i] = events[(int)Random.Range(0, numberOfEvents)];
        }
        for(int j = 0; j < eventAmmount; j++)
        {
            for(int k = 0; k < eventAmmount; k++)
            {
                if(eventArray[j] == eventArray[k] && j != k)
                {
                    eventArray[k] = events[(int)Random.Range(0, numberOfEvents)];
                }
            }
        }

        
    }
}
