using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyIron : MonoBehaviour
{
    [SerializeField] public TMP_InputField quantityInputField;


    public void buyIron()
    {
        FileHandlerStory.Instance.gameData.AddIron(int.Parse(quantityInputField.text));
        FileHandlerStory.Instance.gameData.SubtractMoney(FileHandlerStory.Instance.gameData.ironPrice);
        Debug.Log(FileHandlerStory.Instance.gameData.ironPrice);
    }
}
