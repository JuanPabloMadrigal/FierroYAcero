using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyCoque : MonoBehaviour
{
    [SerializeField] private TMP_InputField quantityInputField;
    [SerializeField] private GameObject buyBtn;
    public int coqueToBuy = 0;
    private bool canBuy;

    private void Start()
    {
        quantityInputField.onValueChanged.AddListener(delegate { checkCanBuy(); });
        canBuy = true;
        checkCanBuy();
    }

    public void buyCoque()
    {
        if (canBuy)
        {
            TurnManager.Instance.coqueToAdd = (int.Parse(quantityInputField.text));
            TurnManager.Instance.moneyToSubtract = (FileHandlerStory.Instance.gameData.coquePrice * int.Parse(quantityInputField.text));
            Debug.Log($"Coque vendido a {FileHandlerStory.Instance.gameData.coquePrice}");
            EconomyTracker.Instance.AddCoqueCounter(int.Parse(quantityInputField.text));
            //EconomyTracker.Instance.AddCoqueCounter(quantityInputField.text);
            Debug.Log(FileHandlerStory.Instance.gameData.coquePrice);
        }
    }

    public void addCoque()
    {
        coqueToBuy = int.Parse(quantityInputField.text);
        coqueToBuy++;
        quantityInputField.text = coqueToBuy.ToString();
    }

    public void subCoque()
    {
        if(coqueToBuy == 0)
        {
            return; 
        }
        coqueToBuy = int.Parse(quantityInputField.text);
        coqueToBuy--;
        quantityInputField.text = coqueToBuy.ToString();
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
        else
        {
            canBuy = false;
            Color btnColor = buyBtn.GetComponent<Image>().color;
            btnColor.a = 0.5f;
            buyBtn.GetComponent<Image>().color = btnColor;
        }
    }

}
