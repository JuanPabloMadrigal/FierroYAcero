using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuyIron : MonoBehaviour
{
    [SerializeField] public TMP_InputField quantityInputField;
    [SerializeField] private GameObject buyBtn;
    [SerializeField] private TMP_Text note;
    private bool canBuy;
    public int ironToBuy = 0;


    private void Start()
    {
        quantityInputField.onValueChanged.AddListener(delegate { checkCanBuy(); });
        if (TurnManager.Instance.ironToAdd > 0) {
            note.text = $"Se estableció una orden de {TurnManager.Instance.ironToAdd} unidades de hierro.";
        }
        else
        {
            note.text = "";
        }
        canBuy = true;
        checkCanBuy();
    }   

    public void buyIron()
    {
        if (canBuy)
        {
            TurnManager.Instance.ironToAdd = (int.Parse(quantityInputField.text));
            TurnManager.Instance.moneyToSubtract = (FileHandlerStory.Instance.gameData.ironMoneyPrice * int.Parse(quantityInputField.text));
            if (TurnManager.Instance.ironToAdd > 0)
            {
                note.text = $"Se estableció una orden de {TurnManager.Instance.ironToAdd} unidades de hierro.";
            }
            else
            {
                note.text = "";
            }
            //EconomyTracker.Instance.AddIronCounter(int.Parse(quantityInputField.text));
        }
    }

    public void addIron()
    {
        ironToBuy = int.Parse(quantityInputField.text);
        ironToBuy++;
        quantityInputField.text = ironToBuy.ToString();
    }

    public void subIron()
    {
        if (ironToBuy == 0)
        {
            return;
        }
        ironToBuy = int.Parse(quantityInputField.text);
        ironToBuy--;
        quantityInputField.text = ironToBuy.ToString();
    }

    public void checkCanBuy()
    {
        if (FileHandlerStory.Instance.gameData.money - (FileHandlerStory.Instance.gameData.ironMoneyPrice * int.Parse(quantityInputField.text)) - TurnManager.Instance.moneyToSubtract >= 0)
        {
            canBuy = true;
            Color btnColor = buyBtn.GetComponent<Image>().color;
            btnColor.a = 1f;
            buyBtn.GetComponent<Image>().color = btnColor;
        }
        else { 
            canBuy = false;
            Color btnColor = buyBtn.GetComponent<Image>().color;
            btnColor.a = 0.5f;
            buyBtn.GetComponent<Image>().color = btnColor;
        }
    }

}
