using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FabricateProducts : MonoBehaviour
{
    [SerializeField] public TMP_InputField quantityInputField;
    public int productToBuy = 0;

    public void fabricateProduct(string product)
    {
        switch (product)
        {
            case "acero":
                FileHandlerStory.Instance.gameData.AddSteel(int.Parse(quantityInputField.text));
                FileHandlerStory.Instance.gameData.SubtractIron(FileHandlerStory.Instance.gameData.steelIronPrice*productToBuy);
                break;
            
        }
    }
}
