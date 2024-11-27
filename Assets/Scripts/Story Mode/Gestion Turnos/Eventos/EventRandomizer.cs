using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EventRandomizer : MonoBehaviour
{

    [SerializeField] private EventView evtView;

    public EventModel[] events;

    void Start()
    {
        CreateTestEvents();
    }

    private void CreateTestEvents()
    {
        events = new EventModel[10];

        // Money Events
        EventModel evt1 = new EventModel();
        evt1.name = "NUEVOS INVERSORES EN LA FUNDIDORA DE FIERRO Y ACERO";
        evt1.description = "¡Buenas noticias para la compañía siderúgica más importante de latinoamerica! Distintos empresarios mexicanos, junto con el gobernador del estado de Nuevo León, han decidido invertir una cantidad de 10,000K pesos mexicanos en la planta Fundidora gracias al potencial que ha demostrado tener en el mercado de acero. Se estima un abono importante para su capital, lo que beneficiará al funcionamiento de la empresa.";
        evt1.triggerType = "money low";
        evt1.resultModifier = 10000;
        events[0] = evt1;

        EventModel evt2 = new EventModel();
        evt2.name = "UN DÍA DE SUERTE PARA EL DIRECTOR DE LA FUNDIDORA";
        evt2.description = "Se ha dado a conocer que el director principal de la Fundidora de Fierro y Acero de Monterrey ha sido el ganador del último sorteo de la Lotería Nacional para la Asistencia Pública, quien obtuvo un premio de 3,000K pesos mexicanos. El director ha declarado que dirigirá todo el dinero a la su compañía, mostrando no solo que se interesa por las causas de organizaciones de este tipo, sino también su determinación por hacer desarrollar la compañía.";
        evt2.triggerType = "money low";
        evt2.resultModifier = 3000;
        events[1] = evt2;

        EventModel evt3 = new EventModel();
        evt3.name = "GRAN FRACASO DE LA FUNDIDORA CON INVERSIONISTAS";
        evt3.description = "El Banco Nacional Mexicano ha informado al público que la Fundidora de Fierro y Acero de Monterrey cuenta con un saldo negativo en su cuenta después de no haber logrado pagar algunas deudas. Se rumorea que la posible causa del acontecimiento podría ser la realización no supervisada y mal planeada de un evento de inversión realizado por un grupo de trabajadores ignorantes en la compañía, que los llevó a presentar metas imposibles de lograr. La compañía se verá obligada a pagar una cantidad de 5,000K pesos mexicanos para evitar problema.";
        evt3.triggerType = "money high";
        evt3.resultModifier = -5000;
        events[2] = evt3;

        EventModel evt31 = new EventModel();
        evt31.name = "TRABAJADORES DEL SECTOR DE ACERO EN HUELGA";
        evt31.description = "Varios trabajadores que laburan para la industria del acero han acordado realizar diferentes huelgas a largo y ancho del país para exigir mejores salarios y mayor seguridad médica en sus compañías, y los trabajadores de la Fundidora de Monterrey no fueron la exceplción. Para evitar que la situación se salga de control y mostrarles a sus empleados que a la compañía le importan, el director de la empresa a dado un comunicado estableciendo que se hará una donación de 3,000K pesos para los servicios médicos.";
        evt31.triggerType = "money high";
        evt31.resultModifier = -3000;
        events[3] = evt31;

        EventModel evt41 = new EventModel();
        evt41.name = "ROBO EN LAS INSTALACIONES DE LA FUNDIDORA";
        evt41.description = "De acuerdo con las autoridades del estado, un grupo de visitantes nocturnos lograron robar algunas de las exportaciones realizadas por la Fundidora de Fierro y Acero de Monterrey. Esto ha evitado que varios encargos llegaran a sus compradores, por lo que se han rehusado a pagarlos. Se estima que la compañía ha perdido 1,500K de los ingresos del día. El director ha comunicado a la prensa que buscará mejorar la seguridad de las instalaciones por el bien de la Fundidora y por el bien de sus trabajadores.";
        evt41.triggerType = "money high";
        evt41.resultModifier = -1500;
        events[4] = evt41;

        EventModel evt42 = new EventModel();
        evt42.name = "FUGA DE GAS EN LA FUNDIDORA";
        evt42.description = "El día de ayer, la Fundidora de Monterrey fué víctima de una fuga de gas dentro de la planta, afortunadamente sin consecuencias mayores. Trabajadores en la escena declararón que todos salieron ilesos del lugar, pero que el evento ha formado un ambiente de inseguridad dentro de las instalaciones. La compañía anunció que invertirá 2,000K en revisiones de seguridad para evitar problemas en el futuro.";
        evt42.triggerType = "money high";
        evt42.resultModifier = -2000;
        events[5] = evt42;

        // Coque Events
        EventModel evt43 = new EventModel();
        evt43.name = "PROBLEMAS DE INVENTARIO EN LA FUNDIDORA";
        evt43.description = "Tras un recuento en el inventario, la Fundidora de Monterrey ha descubierto que tienen un desfase de 5 toneladas de coque en su almacén. De acuerdo con un comunicado que se ha filtrado de parte del director de la empresa, alguien confundió la realización de pedidos de coque con pedidos de cientos de kilos de chocolate, aunque el culpable no ha sido encontrado. Esperamos que este evento no tenga grandes repercusiones en su conteo de material disponible.";
        evt43.triggerType = "coque";
        evt43.resultModifier = -5;
        events[6] = evt43;

        EventModel evt44 = new EventModel();
        evt44.name = "PRECIO DE COMPRA DE COQUE EN GRAN AUMENTO";
        evt44.description = "Aviso urgente: el mercado de coque ha experimentado un alza en los precios debido a factores externos relacionados a su disponibilidad y demanda. Se espera que el costo de este recurso aumente 5K pesos mexicanos, se recomienda a todo el sector industrial ajustar el presupuesto y la frecuencia de compra del material para minimizar el impacto.";
        evt44.triggerType = "coque price";
        evt44.resultModifier = 50;
        events[7] = evt44;

        // Neutral Events
        EventModel evt45 = new EventModel();
        evt45.name = "AUMENTO EN LA DEMANADA DE ACERO EN EL PAÍS";
        evt45.description = "¡Atención! Hay una gran demanda de acero debido a un proyecto de infraestructura en la ciudad. Durante un tiempo limitado, el precio de venta de productos de acero baratos subirá a 200k. No obstante, no es probable que estas condiciones se mantengan por mucho tiempo, por lo que se debería aprovechar la ocurrencia para vender este producto a montones.";
        evt45.triggerType = "steel iron price";
        evt45.resultModifier = 200;
        events[8] = evt45;

        EventModel evt46 = new EventModel();
        evt46.name = "ÉPOCAS TRANQUILAS EN EL SECTOR DE ACERO";
        evt46.description = "Parece que no habrán más imprevistos que alteren el mercado del acero durante un tiempo. Es buen tiempo para que las compañías se enfoquen en sus condiciones actuales para salir adelante y poder enfrentar complicaciones en el futuro.";
        evt46.triggerType = "neutral";
        evt46.resultModifier = 0;
        events[9] = evt46;
    }

    public void GetRandomEvent()
    {

        if (FileHandlerStory.Instance.gameData.evento < 6)
        {
            return;
        }

        Debug.Log($"Número de turnos sin obtener eventos: {FileHandlerStory.Instance.gameData.turnsWithoutEvent}");

        // Se establece la probabilidad de que aparezca un evento
        float evtProb;

        if (FileHandlerStory.Instance.gameData.turnsWithoutEvent == 0)
        {
            evtProb = 0.2f;
        }
        else if (FileHandlerStory.Instance.gameData.turnsWithoutEvent == 1)
        { 
            evtProb = 0.3f;
        }
        else if (FileHandlerStory.Instance.gameData.turnsWithoutEvent == 2)
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
                if (randomEvent.triggerType.ToLower() == "money low" || randomEvent.triggerType.ToLower() == "money high") 
                {
                    FileHandlerStory.Instance.gameData.AddMoney(randomEvent.resultModifier); 
                }
                else if (randomEvent.triggerType.ToLower() == "coque")
                {
                    FileHandlerStory.Instance.gameData.AddCoque(randomEvent.resultModifier);
                }
                else if (randomEvent.triggerType.ToLower() == "coque price")
                {
                    FileHandlerStory.Instance.gameData.SetCoquePrice(randomEvent.resultModifier);
                }
                else if (randomEvent.triggerType.ToLower() == "steel iron price")
                {
                    FileHandlerStory.Instance.gameData.SetSteelIronPrice(randomEvent.resultModifier);
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

                FileHandlerStory.Instance.gameData.turnsWithoutEvent = 0;

            }
            else
            {
                Debug.Log("Error: No event was found.");
            }
        }
        else
        {
            Debug.Log("No event was generated!");
            FileHandlerStory.Instance.gameData.turnsWithoutEvent++;
        }
    }

    public EventModel SetRandomEvent()
    {
        // Create lists for different event types
        List<EventModel> moneyLowEvents = new List<EventModel>();
        List<EventModel> moneyHighEvents = new List<EventModel>();
        List<EventModel> inventoryEvents = new List<EventModel>();
        List<EventModel> sellingEvents = new List<EventModel>();
        List<EventModel> neutralEvents = new List<EventModel>();

        // Categorize events
        foreach (EventModel evt in events)
        {
            switch (evt.triggerType.ToLower())
            {
                case "money low":
                    moneyLowEvents.Add(evt);
                    break;
                case "money high":
                    moneyHighEvents.Add(evt);
                    break;
                case "coque":
                    inventoryEvents.Add(evt);
                    break;
                case "coque price":
                    inventoryEvents.Add(evt);
                    break;
                case "steel iron price":
                    sellingEvents.Add(evt);
                    break;
                default:
                    neutralEvents.Add(evt);
                    break;
            }
        }

        // Calculate probabilities based on player stats
        float moneyHighProbability = CalculateMoneyEventProbability();
        float moneyLowProbability = 0.4f - moneyHighProbability;
        float inventoryProbability = CalculateInventoryEventProbability();
        float sellingProbability = CalculateSellingEventProbability();
        float neutralProbability = 1f - (moneyHighProbability + inventoryProbability + moneyLowProbability + sellingProbability);

        Debug.Log($"MoneyHigh prob: {moneyHighProbability}");
        Debug.Log($"Inventory prob: {inventoryProbability}");
        Debug.Log($"Selling prob: {sellingProbability}");
        Debug.Log($"Neutral prob: {neutralProbability}");

        // Roll for event type
        float roll = Random.value;

        Debug.Log($"Prob de tipo de evento: {roll}");

        if (roll < moneyHighProbability && moneyHighEvents.Count > 0)
        {
            //Debug.Log("Money high event selected");
            return moneyHighEvents[Random.Range(0, moneyHighEvents.Count)];
        }
        else if (roll < moneyHighProbability + moneyLowProbability && moneyLowEvents.Count > 0)
        {
            //Debug.Log("Money low event selected");
            return moneyLowEvents[Random.Range(0, moneyLowEvents.Count)];
        }
        else if (roll < moneyHighProbability + moneyLowProbability + inventoryProbability && inventoryEvents.Count > 0)
        {
            //Debug.Log("Inventory event selected");
            return inventoryEvents[Random.Range(0, inventoryEvents.Count)];
        }
        else if (roll < moneyHighProbability + moneyLowProbability + inventoryProbability + sellingProbability && sellingEvents.Count > 0)
        {
            //Debug.Log("Selling event selected");
            return sellingEvents[Random.Range(0, sellingEvents.Count)];
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
        // Base probability is 0.2, increases up to 0.3 based on money amount
        float baseProbability = 0.15f;
        float maxProbability = 0.3f;

        // Consider money above 10000 as "high"
        float moneyFactor = Mathf.Clamp01(FileHandlerStory.Instance.gameData.money / 10000f);
        
        return Mathf.Lerp(baseProbability, maxProbability, moneyFactor);
    }

    private float CalculateInventoryEventProbability()
    {
        // Base probability is 0.2, increases up to 0.3 based on coque amount
        float baseProbability = 0.2f;
        float maxProbability = 0.3f;
        
        // Consider coque above 20 as "high"
        float inventoryFactor = Mathf.Clamp01((FileHandlerStory.Instance.gameData.coque + FileHandlerStory.Instance.gameData.iron) / 20f);
        
        return Mathf.Lerp(baseProbability, maxProbability, inventoryFactor);
    }

    private float CalculateSellingEventProbability()
    {
        // Base probability is 0.2, increases up to 0.3 based on coque amount
        float baseProbability = 0.1f;
        float maxProbability = 0.2f;

        // Consider coque above 10 as "high"
        float sellingFactor = Mathf.Clamp01((FileHandlerStory.Instance.gameData.steel) / 10f);

        return Mathf.Lerp(baseProbability, maxProbability, sellingFactor);
    }
}
