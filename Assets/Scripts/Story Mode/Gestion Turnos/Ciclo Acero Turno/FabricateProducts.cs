
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class FabricateProducts : MonoBehaviour
{
    [SerializeField] public TMP_InputField quantityInputFieldAcero;
    [SerializeField] public TMP_Text steelCostUI;
    [SerializeField] public TMP_Text steelBarCostUI;
    [SerializeField] public TMP_Text railCostUI;
    [SerializeField] public TMP_Text debugUI;
    private List<ProductCostValues> productCostValues;

    public int productToBuy = 0;

    private void Start()
    {
        productCostValues = new List<ProductCostValues>() { new ProductCostValues("Alambrado de acero", FileHandler.Instance.gameData.ironPrice) };
    }

    public void fabricateProduct(string product)
    {
        switch (product)
        {
            case "acero":
                FileHandlerStory.Instance.gameData.AddSteel(int.Parse(quantityInputFieldAcero.text));
                FileHandlerStory.Instance.gameData.SubtractIron(FileHandlerStory.Instance.gameData.steelIronPrice*productToBuy);
                FileHandlerStory.Instance.gameData.SubtractCoque(2 * int.Parse(quantityInputFieldAcero.text));
                //debugUI.text = $"Acero generado: {FileHandlerStory.Instance.gameData.steel}";
                break;
            case "barra":
                FileHandlerStory.Instance.gameData.AddSteelBar(int.Parse(quantityInputFieldAcero.text));
                FileHandlerStory.Instance.gameData.SubtractIron(FileHandlerStory.Instance.gameData.steelIronPrice * productToBuy);
                FileHandlerStory.Instance.gameData.SubtractCoque(2 * int.Parse(quantityInputFieldAcero.text));
                //debugUI.text = $"Acero generado: {FileHandlerStory.Instance.gameData.steel}";
                break;
            case "riel":
                FileHandlerStory.Instance.gameData.AddRail(int.Parse(quantityInputFieldAcero.text));
                FileHandlerStory.Instance.gameData.SubtractIron(FileHandlerStory.Instance.gameData.steelIronPrice * productToBuy);
                FileHandlerStory.Instance.gameData.SubtractCoque(2 * int.Parse(quantityInputFieldAcero.text));
                //debugUI.text = $"Acero generado: {FileHandlerStory.Instance.gameData.steel}";
                break;

        }
    }
    
    public void updateCostUI(string product)
    {
        switch (product)
        {
            case "acero":
                var ironCost = FileHandlerStory.Instance.gameData.ironPrice * int.Parse(quantityInputFieldAcero.text);
                var coqueCost = ironCost / 2;
                steelCostUI.text = $"Hierro: {ironCost} \n Coque: {coqueCost}";
                break;
            case "barra":
                var barCost = Mathf.RoundToInt(FileHandlerStory.Instance.gameData.steelIronPrice * 1.5f * int.Parse(quantityInputFieldAcero.text));
                var coqueBarCost = barCost / 2;
                steelBarCostUI.text = $"Hierro: {barCost} \n Coque: {coqueBarCost}";
                break;
            case "riel":
                var railCost = Mathf.RoundToInt(FileHandlerStory.Instance.gameData.steelIronPrice * 2f * int.Parse(quantityInputFieldAcero.text));
                var coqueRailCost = railCost / 2;
                railCostUI.text = $"Hierro: {railCost} \n Coque: {coqueRailCost}";
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
            case "barra":
                productToBuy = int.Parse(quantityInputFieldAcero.text);
                productToBuy++;
                quantityInputFieldAcero.text = productToBuy.ToString();
                break;
            case "riel":
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

[System.Serializable]
public class ProductCostValues
{
    public string title;
    public int costIron;

    public ProductCostValues(string title, int costIron)
    {
        this.title = title;
        this.costIron = costIron;
    }

}
