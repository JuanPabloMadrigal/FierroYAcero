using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EventRandomizer : MonoBehaviour
{
    public EventModel[] events;
    private GameModel gameData;

    void Start()
    {
        gameData = FileHandlerStory.Instance.gameData;
        CreateTestEvents();
        
        // Test the event system every 3 seconds
        InvokeRepeating("TestRandomEvent", 1f, 3f);
    }

    private void CreateTestEvents()
    {
        events = new EventModel[6];

        // Money Events
        GameObject moneyEvent1 = new GameObject("MoneyEvent1");
        EventModel evt1 = moneyEvent1.AddComponent<EventModel>();
        evt1.name = "Tax Collection";
        evt1.description = "The government demands taxes!";
        evt1.triggerType = "money";
        evt1.resultModifier = -200;
        events[0] = evt1;

        GameObject moneyEvent2 = new GameObject("MoneyEvent2");
        EventModel evt2 = moneyEvent2.AddComponent<EventModel>();
        evt2.name = "Market Boom";
        evt2.description = "The market is booming!";
        evt2.triggerType = "money";
        evt2.resultModifier = 300;
        events[1] = evt2;

        // Coque Events
        GameObject coqueEvent1 = new GameObject("CoqueEvent1");
        EventModel evt3 = coqueEvent1.AddComponent<EventModel>();
        evt3.name = "Storage Fire";
        evt3.description = "A fire in the storage destroyed some coque!";
        evt3.triggerType = "coque";
        evt3.resultModifier = -10;
        events[2] = evt3;

        GameObject coqueEvent2 = new GameObject("CoqueEvent2");
        EventModel evt4 = coqueEvent2.AddComponent<EventModel>();
        evt4.name = "Efficient Production";
        evt4.description = "Production efficiency increased!";
        evt4.triggerType = "coque";
        evt4.resultModifier = 15;
        events[3] = evt4;

        // Neutral Events
        GameObject neutralEvent1 = new GameObject("NeutralEvent1");
        EventModel evt5 = neutralEvent1.AddComponent<EventModel>();
        evt5.name = "Worker Meeting";
        evt5.description = "Workers are having a meeting.";
        evt5.triggerType = "neutral";
        evt5.resultModifier = 0;
        events[4] = evt5;

        GameObject neutralEvent2 = new GameObject("NeutralEvent2");
        EventModel evt6 = neutralEvent2.AddComponent<EventModel>();
        evt6.name = "Weather News";
        evt6.description = "It's a sunny day!";
        evt6.triggerType = "neutral";
        evt6.resultModifier = 0;
        events[5] = evt6;
    }

    private void TestRandomEvent()
    {
        EventModel randomEvent = GetRandomEvent();
        if (randomEvent != null)
        {
            Debug.Log($"=== Random Event Generated ===");
            Debug.Log($"Current Stats - Money: {gameData.money}, Coque: {gameData.coque}");
            Debug.Log($"Event Name: {randomEvent.name}");
            Debug.Log($"Event Type: {randomEvent.triggerType}");
            Debug.Log($"Description: {randomEvent.description}");
            Debug.Log($"Modifier: {randomEvent.resultModifier}");
            Debug.Log("===========================");
        }
        else
        {
            Debug.Log("No event was generated!");
        }
    }

    public EventModel GetRandomEvent()
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

        // Roll for event type
        float roll = Random.value;
        
        if (roll < moneyProbability && moneyEvents.Count > 0)
        {
            return moneyEvents[Random.Range(0, moneyEvents.Count)];
        }
        else if (roll < moneyProbability + coqueProbability && coqueEvents.Count > 0)
        {
            return coqueEvents[Random.Range(0, coqueEvents.Count)];
        }
        else if (neutralEvents.Count > 0)
        {
            return neutralEvents[Random.Range(0, neutralEvents.Count)];
        }

        // Fallback if no events are available
        return null;
    }

    private float CalculateMoneyEventProbability()
    {
        // Base probability is 0.2, increases up to 0.5 based on money amount
        float baseProbability = 0.2f;
        float maxProbability = 0.5f;
        
        // Consider money above 1000 as "high"
        float moneyFactor = Mathf.Clamp01(gameData.money / 1000f);
        
        return Mathf.Lerp(baseProbability, maxProbability, moneyFactor);
    }

    private float CalculateCoqueEventProbability()
    {
        // Base probability is 0.2, increases up to 0.5 based on coque amount
        float baseProbability = 0.2f;
        float maxProbability = 0.5f;
        
        // Consider coque above 50 as "high"
        float coqueFactor = Mathf.Clamp01(gameData.coque / 50f);
        
        return Mathf.Lerp(baseProbability, maxProbability, coqueFactor);
    }
}
