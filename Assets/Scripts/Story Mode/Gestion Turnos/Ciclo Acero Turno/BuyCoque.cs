using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyCoque : MonoBehaviour
{
    [SerializeField] private TMP_InputField quantityInputField;


    public void buyCoque()
    {
        FileHandlerStory.Instance.gameData.AddCoque(int.Parse(quantityInputField.text));
        FileHandlerStory.Instance.gameData.SubtractMoney(FileHandlerStory.Instance.gameData.coquePrice);
        Debug.Log(FileHandlerStory.Instance.gameData.coquePrice);
    }
}
