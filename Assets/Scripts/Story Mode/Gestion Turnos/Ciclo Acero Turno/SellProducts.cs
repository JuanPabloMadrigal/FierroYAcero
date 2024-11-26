using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class SellProducts : MonoBehaviour
{
    [SerializeField] public TMP_Text titleProductType;
    [SerializeField] public TMP_InputField quantityInputField;
    [SerializeField] public TMP_Text priceUI;
    [SerializeField] public GameObject sellBtn;
    [SerializeField] public TMP_Text note;

    private List<ProductSaleValues> productSaleValues;
    private ProductSaleValues currentProductShown;
    private int currentProductShownIndex;
    bool canSell;

    public int productToSell = 0;

    private void Start()
    {
        canSell = true;
        productSaleValues = new List<ProductSaleValues>() { 
            new ProductSaleValues("Alambrado de acero", FileHandlerStory.Instance.gameData.steelIronPrice),
            new ProductSaleValues("Barra de acero", FileHandlerStory.Instance.gameData.steelBarPrice)
        };
        
        if (FileHandlerStory.Instance.gameData.evento >= 7)
        {
            productSaleValues.Add(new ProductSaleValues("Riel", FileHandlerStory.Instance.gameData.steelRailPrice));
        }

        currentProductShownIndex = 0;
        UpdateViewByProductType(currentProductShownIndex);
        disableBtn();
        quantityInputField.onValueChanged.AddListener(delegate { checkCanSell(); });

        if (TurnManager.Instance.steelToSubtract > 0 || TurnManager.Instance.steelBarToSubtract > 0 || TurnManager.Instance.railToSubtract > 0)
        {
            note.text = "Existe una orden de venta pendiente.";
        }
    }

    public void sellProduct()
    {
        if (canSell)
        {
            switch (currentProductShown.title)
            {
                case "Alambrado de acero":
                    TurnManager.Instance.steelToSubtract = int.Parse(quantityInputField.text);
                    TurnManager.Instance.steelBarToSubtract = 0;
                    TurnManager.Instance.railToSubtract = 0;
                    TurnManager.Instance.moneyToAdd = (currentProductShown.sellPrice * int.Parse(quantityInputField.text));
                    note.text = "Se ha establecido una nueva orden de venta.";
                    break;
                case "Barra de acero":
                    TurnManager.Instance.steelToSubtract = 0;
                    TurnManager.Instance.steelBarToSubtract = int.Parse(quantityInputField.text);
                    TurnManager.Instance.railToSubtract = 0;
                    TurnManager.Instance.moneyToAdd = (currentProductShown.sellPrice * int.Parse(quantityInputField.text));
                    note.text = "Se ha establecido una nueva orden de venta.";
                    break;
                case "Riel":
                    TurnManager.Instance.steelToSubtract = 0;
                    TurnManager.Instance.steelBarToSubtract = 0;
                    TurnManager.Instance.railToSubtract = int.Parse(quantityInputField.text);
                    TurnManager.Instance.moneyToAdd = (currentProductShown.sellPrice * int.Parse(quantityInputField.text));
                    note.text = "Se ha establecido una nueva orden de venta.";
                    break;
            }
        }
    }

    public void UpdateViewByProductType(int num)
    {
        currentProductShownIndex += num;

        if (currentProductShownIndex >= productSaleValues.Count)
        {
            currentProductShownIndex = 0;
        }
        else if (currentProductShownIndex < 0)
        {
            currentProductShownIndex = productSaleValues.Count - 1;
        }

        currentProductShown = productSaleValues[currentProductShownIndex];
        titleProductType.text = currentProductShown.title;
        updatePriceUI();
    }

    public void updatePriceUI()
    {
        int totalPrice = currentProductShown.sellPrice * int.Parse(quantityInputField.text);
        priceUI.text = $"Precio total: {totalPrice}";
    }

    public void addProduct()
    {
        productToSell = int.Parse(quantityInputField.text);
        productToSell++;
        quantityInputField.text = productToSell.ToString();
        updatePriceUI();
    }

    public void subProduct()
    {
        if (productToSell == 0)
        {
            return;
        }
        productToSell = int.Parse(quantityInputField.text);
        productToSell--;
        quantityInputField.text = productToSell.ToString();
        updatePriceUI();
    }

    public void checkCanSell()
    {
        bool hasEnoughProduct = false;
        
        switch (currentProductShown.title)
        {
            case "Alambrado de acero":
                hasEnoughProduct = FileHandlerStory.Instance.gameData.steel >= int.Parse(quantityInputField.text);
                break;
            case "Barra de acero":
                hasEnoughProduct = FileHandlerStory.Instance.gameData.steelBar >= int.Parse(quantityInputField.text);
                break;
            case "Riel":
                hasEnoughProduct = FileHandlerStory.Instance.gameData.steelRail >= int.Parse(quantityInputField.text);
                break;
        }

        if (hasEnoughProduct && int.Parse(quantityInputField.text) > 0)
        {
            canSell = true;
            Color btnColor = sellBtn.GetComponent<Image>().color;
            btnColor.a = 1f;
            sellBtn.GetComponent<Image>().color = btnColor;
        }
        else
        {
            disableBtn();
        }
    }

    private void disableBtn()
    {
        canSell = false;
        Color btnColor = sellBtn.GetComponent<Image>().color;
        btnColor.a = 0.5f;
        sellBtn.GetComponent<Image>().color = btnColor;
    }
}

[System.Serializable]
public class ProductSaleValues
{
    public string title;
    public int sellPrice;

    public ProductSaleValues(string title, int sellPrice)
    {
        this.title = title;
        this.sellPrice = sellPrice;
    }
}
