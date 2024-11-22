using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuyIron : MonoBehaviour
{
    [SerializeField] public TMP_InputField quantityInputField;
    [SerializeField] private GameObject buyBtn;
    private bool canBuy;
    public int ironToBuy = 0;


    private void Start()
    {
        quantityInputField.onValueChanged.AddListener(delegate { checkCanBuy(); });
        canBuy = true;
        checkCanBuy();
    }

    public void buyIron()
    {
        if (canBuy)
        {
            TurnManager.Instance.ironToAdd = (int.Parse(quantityInputField.text));
            TurnManager.Instance.moneyToSubtract = (FileHandlerStory.Instance.gameData.ironMoneyPrice * int.Parse(quantityInputField.text));
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
        if (FileHandlerStory.Instance.gameData.money - (FileHandlerStory.Instance.gameData.ironMoneyPrice * int.Parse(quantityInputField.text)) - TurnManager.Instance.moneyToSubtract >= 0 && int.Parse(quantityInputField.text) != 0)
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
