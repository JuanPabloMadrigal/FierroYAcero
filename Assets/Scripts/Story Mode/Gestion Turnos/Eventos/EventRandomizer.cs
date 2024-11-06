using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EventRandomizer : MonoBehaviour
{

    [SerializeField] private EventView evtView;

    public EventModel[] events;
    public int turnsWithoutEvent;

    void Start()
    {
        CreateTestEvents();
    }

    private void CreateTestEvents()
    {
        events = new EventModel[6];

        // Money Events
        EventModel evt1 = new EventModel();
        evt1.name = "Tax Collection";
        evt1.description = "The government demands taxes!";
        evt1.triggerType = "money";
        evt1.resultModifier = -200;
        events[0] = evt1;

        EventModel evt2 = new EventModel();
        evt2.name = "Market Boom";
        evt2.description = "The market is booming!";
        evt2.triggerType = "money";
        evt2.resultModifier = 300;
        events[1] = evt2;

        // Coque Events
        EventModel evt3 = new EventModel();
        evt3.name = "Storage Fire";
        evt3.description = "A fire in the storage destroyed some coque!";
        evt3.triggerType = "coque";
        evt3.resultModifier = -10;
        events[2] = evt3;

        EventModel evt4 = new EventModel();
        evt4.name = "Efficient Production";
        evt4.description = "Production efficiency increased!";
        evt4.triggerType = "coque";
        evt4.resultModifier = 15;
        events[3] = evt4;

        // Neutral Events
        EventModel evt5 = new EventModel();
        evt5.name = "Worker Meeting";
        evt5.description = "Workers are having a meeting.";
        evt5.triggerType = "neutral";
        evt5.resultModifier = 0;
        events[4] = evt5;

        EventModel evt6 = new EventModel();
        evt6.name = "Weather News";
        evt6.description = "It's a sunny day!";
        evt6.triggerType = "neutral";
        evt6.resultModifier = 0;
        events[5] = evt6;
    }

    public void GetRandomEvent()
    {

        Debug.Log($"Número de turnos sin obtener eventos: {turnsWithoutEvent}");

        // Se establece la probabilidad de que aparezca un evento
        float evtProb;

        if (turnsWithoutEvent == 0)
        {
            evtProb = 0.2f;
        }
        else if (turnsWithoutEvent == 1)
        { 
            evtProb = 0.3f;
        }
        else if (turnsWithoutEvent == 2)
        {
            evtProb = 0.4f;
        }
        else
        {
            evtProb = 0.6f;
        }

        Debug.Log($"Probabilidad de que aparezca un evento: {evtProb}");

        // Se decide si habrá evento y el tipo de evento
        float probEvtHappening = Random.value;

        if (probEvtHappening < evtProb)
        {
            EventModel randomEvent = SetRandomEvent();

            if (ReferenceEquals(randomEvent, null) != true)
            {

                // Se efectuan cambios
                if (randomEvent.triggerType.ToLower() == "money") 
                {
                    FileHandlerStory.Instance.gameData.AddMoney(randomEvent.resultModifier); 
                }
                else if (randomEvent.triggerType.ToLower() == "coque")
                {
                    FileHandlerStory.Instance.gameData.AddCoque(randomEvent.resultModifier);
                }

                // Se muestra la pantalla del evento
                evtView.ShowEventScreen(randomEvent);

                Debug.Log($"=== Random Event Generated ===");
                Debug.Log($"Current Stats - Money: {FileHandlerStory.Instance.gameData.money}, Coque: {FileHandlerStory.Instance.gameData.coque}");
                Debug.Log($"Event Name: {randomEvent.name}");
                Debug.Log($"Event Type: {randomEvent.triggerType}");
                Debug.Log($"Description: {randomEvent.description}");
                Debug.Log($"Modifier: {randomEvent.resultModifier}");
                Debug.Log("===========================");

                turnsWithoutEvent = 0;

            }
            else
            {
                Debug.Log("Error: No event was found.");
            }
        }
        else
        {
            Debug.Log("No event was generated!");
            turnsWithoutEvent++;
        }
    }

    public EventModel SetRandomEvent()
    {
        // Create lists for different event types
        List<EventModel> moneyEvents = new List<EventModel>();
        List<EventModel> coqueEvents = new List<EventModel>();
        List<EventModel> neutralEvents = new List<EventModel>();

        // Categorize events
        foreach (EventModel evt in events)
        {
            switch (evt.triggerType.ToLower())
            {
                case "money":
                    moneyEvents.Add(evt);
                    break;
                case "coque":
                    coqueEvents.Add(evt);
                    break;
                default:
                    neutralEvents.Add(evt);
                    break;
            }
        }

        // Calculate probabilities based on player stats
        float moneyProbability = CalculateMoneyEventProbability();
        float coqueProbability = CalculateCoqueEventProbability();
        float neutralProbability = 1f - (moneyProbability + coqueProbability);

        Debug.Log($"Money prob: {moneyProbability}");
        Debug.Log($"Coque prob: {coqueProbability}");
        Debug.Log($"Neutral prob: {neutralProbability}");

        // Roll for event type
        float roll = Random.value;

        Debug.Log($"Prob de tipo de evento: {roll}");

        if (roll < moneyProbability && moneyEvents.Count > 0)
        {
            //Debug.Log("Money event selected");
            return moneyEvents[Random.Range(0, moneyEvents.Count)];
        }
        else if (roll < moneyProbability + coqueProbability && coqueEvents.Count > 0)
        {
            //Debug.Log("Coque event selected");
            return coqueEvents[Random.Range(0, coqueEvents.Count)];
        }
        else if (neutralEvents.Count > 0)
        {
            //Debug.Log("Neutral event selected");
            return neutralEvents[Random.Range(0, neutralEvents.Count)];
        }

        // Fallback if no events are available
        return null;
    }

    private float CalculateMoneyEventProbability()
    {
        // Base probability is 0.2, increases up to 0.5 based on money amount
        float baseProbability = 0.3f;
        float maxProbability = 0.5f;

        // Consider money above 1000 as "high"
        float moneyFactor = Mathf.Clamp01(FileHandlerStory.Instance.gameData.money / 1000f);
        
        return Mathf.Lerp(baseProbability, maxProbability, moneyFactor);
    }

    private float CalculateCoqueEventProbability()
    {
        // Base probability is 0.2, increases up to 0.5 based on coque amount
        float baseProbability = 0.1f;
        float maxProbability = 0.3f;
        
        // Consider coque above 50 as "high"
        float coqueFactor = Mathf.Clamp01(FileHandlerStory.Instance.gameData.coque / 50f);
        
        return Mathf.Lerp(baseProbability, maxProbability, coqueFactor);
    }
}
