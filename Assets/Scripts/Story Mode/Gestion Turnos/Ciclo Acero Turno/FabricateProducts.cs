using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class FabricateProducts : MonoBehaviour
{
    [SerializeField] public TMP_InputField quantityInputFieldAcero;
    [SerializeField] public TMP_Text steelCostUI;
    [SerializeField] public TMP_Text debugUI;

    public int productToBuy = 0;
    

    public void fabricateProduct(string product)
    {
        switch (product)
        {
            case "acero":
                FileHandlerStory.Instance.gameData.AddSteel(int.Parse(quantityInputFieldAcero.text));
                FileHandlerStory.Instance.gameData.SubtractIron(FileHandlerStory.Instance.gameData.steelIronPrice*productToBuy);
                FileHandlerStory.Instance.gameData.SubtractCoque(2 * int.Parse(quantityInputFieldAcero.text));
                debugUI.text = $"Acero generado: {FileHandlerStory.Instance.gameData.steel}";
                EconomyTracker.Instance.AddSteelCounter(int.Parse(quantityInputFieldAcero.text));
                break;
        }
    }
    
    public void updateCostUI(string product)
    {
        switch (product)
        {
            case "acero":
                var ironCost = FileHandlerStory.Instance.gameData.steelIronPrice * int.Parse(quantityInputFieldAcero.text);
                var coqueCost = 2 * int.Parse(quantityInputFieldAcero.text);
                steelCostUI.text = $"Hierro: {ironCost} \n Coque: {coqueCost}";
                break;
        }
    }

    public void addProduct(string product)
    {
        switch (product)
        {
            case "acero":
                productToBuy = int.Parse(quantityInputFieldAcero.text);
                productToBuy++;
                quantityInputFieldAcero.text = productToBuy.ToString();
                break;
        }
        
    }

    public void subProduct(string product)
    {
        switch (product)
        {
            case "acero":
                if (productToBuy == 0)
                {
                    return;
                }
                productToBuy = int.Parse(quantityInputFieldAcero.text);
                productToBuy--;
                quantityInputFieldAcero.text = productToBuy.ToString();
                break;
        }
        
    }
}
