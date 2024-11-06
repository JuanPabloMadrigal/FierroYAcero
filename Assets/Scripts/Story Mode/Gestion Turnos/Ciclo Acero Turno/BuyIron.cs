using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyIron : MonoBehaviour
{
    [SerializeField] public TMP_InputField quantityInputField;
    public int ironToBuy = 0;


    public void buyIron()
    {
        FileHandlerStory.Instance.gameData.AddIron(int.Parse(quantityInputField.text));
        FileHandlerStory.Instance.gameData.SubtractMoney(FileHandlerStory.Instance.gameData.ironPrice * int.Parse(quantityInputField.text));
        Debug.Log(FileHandlerStory.Instance.gameData.ironPrice);
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
}
