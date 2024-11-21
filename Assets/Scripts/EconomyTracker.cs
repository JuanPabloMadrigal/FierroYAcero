using UnityEngine;
using TMPro;

public class EconomyTracker : MonoBehaviour
{
    [SerializeField]
    private GameObject statsPanel; 

    [SerializeField]
    private TMP_Text statsText; 

    public static EconomyTracker Instance;

    // Referencia a los datos del juego y al TurnManager
    [SerializeField]
    private GameModel gameData;
    [SerializeField]
    private TurnManager turnManager;
    [SerializeField]
    private BuyCoque buycoque;

    // Variables para trackear datos por turno
    private int turnSpentMoney = 0;
    private int turnGainedMoney = 0;
    private int turnCoquePurchased = 0;
    private int turnSteelProduced = 0;
    private int turnIronPurchased = 0;
    private int currentMoney = 0;
    private int totalCoquePurchased = 0; // Coque total comprado en toda la partida
    private int totalIronPurchased = 0; // Hierro total comprado en toda la partida
    private int totalSteelProduced = 0;

    private int currentTurn = 0; // Inicializar variable de contador de turnos

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        gameData = FileHandlerStory.Instance.gameData;
        turnManager = FindObjectOfType<TurnManager>();  
        statsPanel.SetActive(false);

    }

    public void AddCoqueCounter(int quantity)
    {
        totalCoquePurchased += quantity;
        turnCoquePurchased += quantity;
    }

    public void AddIronCounter(int quantity)
    {
        totalIronPurchased += quantity;
        turnIronPurchased += quantity;
    }

    public void AddSteelCounter(int quantity)
    {
        totalSteelProduced += quantity;
        turnSteelProduced += quantity;
    }

    // Método para incrementar el contador de turnos
    public void IncrementTurn()
    {
        currentTurn++;
    }

    // Método para obtener el turno actual
    public int GetCurrentTurn()
    {
        return currentTurn;
    }

    // Desplegar estadisticas de cada turno
    public void DisplayEconomyStats()
    {
       // turnSpentMoney = turnManager.turnDeficit;
        //turnGainedMoney = turnManager.turnProfit;
        currentMoney = FileHandlerStory.Instance.gameData.money;
        turnCoquePurchased = totalCoquePurchased;
        turnIronPurchased = totalIronPurchased;
        turnSteelProduced = FileHandlerStory.Instance.gameData.steel;

        // Actualizar el texto 
        statsText.text = $"                             Estadísticas del Turno {currentTurn}        \n" +
                         //$"Dinero ganado en el turno: {turnGainedMoney}\n" +
                         //$"Dinero gastado en el turno: {turnSpentMoney}\n" +
                         $"Coque comprado en este turno: {totalCoquePurchased} unidades\n" +
                         $"Hierro comprado en este turno: {totalIronPurchased} unidades\n" +
                         $"Acero producido en este turno: {totalSteelProduced} unidades\n" +
                         $"Dinero total: {currentMoney}\n" ;

        // Mostrar texto
        statsPanel.SetActive(true);

        // Ocultar el panel después de unos segundos
        //Invoke(nameof(HideStatsPanel), 10f);
        ResetTurnStats();
    }
/*
    private void HideStatsPanel()
    {
        statsPanel.SetActive(false);
    }
*/
    public void Quit()
    {
        statsPanel.SetActive(false);
    }

    // Método para actualizar las estadísticas cada vez que el turno termine 
    public void ResetTurnStats()
    {
        turnSpentMoney = 0;
        turnGainedMoney = 0;
        turnSteelProduced = 0;
        totalCoquePurchased = 0; 
        totalIronPurchased = 0;
        totalSteelProduced = 0;
    }


}
