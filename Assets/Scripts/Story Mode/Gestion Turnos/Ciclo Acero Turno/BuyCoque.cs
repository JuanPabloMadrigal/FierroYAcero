using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyCoque : MonoBehaviour
{
    [SerializeField] private TMP_InputField quantityInputField;
    public int coqueToBuy = 0;

    public void buyCoque()
    {
        FileHandlerStory.Instance.gameData.AddCoque(int.Parse(quantityInputField.text));
        FileHandlerStory.Instance.gameData.SubtractMoney(FileHandlerStory.Instance.gameData.coquePrice * int.Parse(quantityInputField.text));
        Debug.Log(FileHandlerStory.Instance.gameData.coquePrice);
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
}
