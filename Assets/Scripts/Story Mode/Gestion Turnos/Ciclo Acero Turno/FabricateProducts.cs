
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class FabricateProducts : MonoBehaviour
{

    [SerializeField] public TMP_Text titleProductType;
    [SerializeField] public TMP_InputField quantityInputFieldAcero;
    [SerializeField] public TMP_Text costUI;
    [SerializeField] public GameObject produceBtn;
    [SerializeField] public TMP_Text note;

    private List<ProductCostValues> productCostValues;
    private ProductCostValues currentProductShown;
    private int currentProductShownIndex;
    bool canProduce;

    public int productToBuy = 0;

    private void Start()
    {
        canProduce = true;
        productCostValues = new List<ProductCostValues>() { new ProductCostValues("Alambrado de acero", FileHandlerStory.Instance.gameData.ironPrice), new ProductCostValues("Barra de acero", FileHandlerStory.Instance.gameData.ironPrice * 1.5f)};
        if (FileHandlerStory.Instance.gameData.evento >= 7)
        {
            productCostValues.Add(new ProductCostValues("Riel", FileHandlerStory.Instance.gameData.ironPrice * 2f));
        }
        currentProductShownIndex = 0;
        UpdateViewByProductType(currentProductShownIndex);
        disableBtn();
        quantityInputFieldAcero.onValueChanged.AddListener(delegate { checkCanProduce(); } );

        if (TurnManager.Instance.steelToAdd > 0 || TurnManager.Instance.steelBarToAdd > 0 || TurnManager.Instance.railToAdd > 0) 
        {
            note.text = "Existe una orden pendiente.";
        }

    }

    public void fabricateProduct()
    {
        Debug.Log(currentProductShown.title);
        if (canProduce)
        {
            switch (currentProductShown.title)
            {
                case "Alambrado de acero":
                    TurnManager.Instance.steelToAdd = int.Parse(quantityInputFieldAcero.text);
                    TurnManager.Instance.steelBarToAdd = 0;
                    TurnManager.Instance.railToAdd = 0;
                    TurnManager.Instance.ironToSubtract = (getNeededIronQuantity(currentProductShown.costIron));
                    TurnManager.Instance.coqueToSubtract = TurnManager.Instance.ironToSubtract / 2;
                    note.text = "Se ha establecido una nueva orden de producción.";
                    Debug.Log(TurnManager.Instance.steelToAdd);
                    break;
                case "Barra de acero":
                    TurnManager.Instance.steelToAdd = 0;
                    TurnManager.Instance.steelBarToAdd = int.Parse(quantityInputFieldAcero.text);
                    TurnManager.Instance.railToAdd = 0;
                    TurnManager.Instance.ironToSubtract = (getNeededIronQuantity(currentProductShown.costIron));
                    TurnManager.Instance.coqueToSubtract = TurnManager.Instance.ironToSubtract / 2;
                    note.text = "Se ha establecido una nueva orden de producción.";
                    Debug.Log(TurnManager.Instance.steelBarToAdd);
                    break;
                case "Riel":
                    TurnManager.Instance.steelToAdd = 0;
                    TurnManager.Instance.steelBarToAdd = 0;
                    TurnManager.Instance.railToAdd = int.Parse(quantityInputFieldAcero.text);
                    TurnManager.Instance.ironToSubtract = (getNeededIronQuantity(currentProductShown.costIron));
                    TurnManager.Instance.coqueToSubtract = TurnManager.Instance.ironToSubtract / 2;
                    note.text = "Se ha establecido una nueva orden de producción.";
                    Debug.Log(TurnManager.Instance.railToAdd);
                    break;
            }
        }
    }

    public int getNeededIronQuantity(int baseCost)
    {

        int resultQuantity = 0;

        /*foreach (BuildingProperties building in FileHandlerStory.Instance.gameData.buildingsList)
        {
            if (building.unlocked)
            {
                float buildingWorkersCalculations = Mathf.FloorToInt(building.workersNum / FileHandlerStory.Instance.gameData.descuentoEmpleados);
                //Debug.Log($"calculo workers: {buildingWorkersCalculations}, tipo: {building.type}");
                if (buildingWorkersCalculations == 0)
                {
                    disableBtn();
                    note.text = $"Algún edificio tiene menos de {FileHandlerStory.Instance.gameData.descuentoEmpleados} trabajadores.";
                    return 0;
                }
                int ironQuantity = Mathf.RoundToInt(((int.Parse(quantityInputFieldAcero.text)) * baseCost) / (building.addingValue * building.valueModifier * Mathf.FloorToInt(building.workersNum / FileHandlerStory.Instance.gameData.descuentoEmpleados)));
                resultQuantity += ironQuantity;
            }
        }

        note.text = "";*/

        float totalWorkersFactor = 0f;

        foreach (BuildingProperties building in FileHandlerStory.Instance.gameData.buildingsList)
        {
            if (building.unlocked)
            {

                float buildingWorkersFactor = Mathf.FloorToInt(building.workersNum / FileHandlerStory.Instance.gameData.descuentoEmpleados);
                if (buildingWorkersFactor == 0)
                {
                    disableBtn();
                    note.text = $"Algún edificio tiene menos de {FileHandlerStory.Instance.gameData.descuentoEmpleados} trabajadores.";
                    return 0;
                }
                totalWorkersFactor += buildingWorkersFactor * (building.addingValue * building.valueModifier);
            }
        }

        int baseQuantity = int.Parse(quantityInputFieldAcero.text);
        int ironQuantity = Mathf.RoundToInt((20 * baseQuantity * baseCost) / totalWorkersFactor);

        resultQuantity += ironQuantity;

        return resultQuantity;
    }

    public void UpdateViewByProductType(int num)
    {
        currentProductShownIndex += num;

        if (currentProductShownIndex >= productCostValues.Count)
        {
            currentProductShownIndex = 0;
        }
        else if (currentProductShownIndex < 0)
        {
            currentProductShownIndex = productCostValues.Count - 1;
        }

        currentProductShown = productCostValues[currentProductShownIndex];

        titleProductType.text = currentProductShown.title;

        updateCostUI();


    }

    public void updateCostUI()
    {

        int ironCost = getNeededIronQuantity(currentProductShown.costIron);
        //int ironCost = currentProductShown.costIron * int.Parse(quantityInputFieldAcero.text);
        int coqueCost = ironCost / 2;
        costUI.text = $"Hierro: {ironCost} \n Coque: {coqueCost}";

    }

    public void addProduct()
    {
        productToBuy = int.Parse(quantityInputFieldAcero.text);
        productToBuy++;
        quantityInputFieldAcero.text = productToBuy.ToString();
        updateCostUI();
    }

    public void subProduct()
    {
        if (productToBuy == 0)
        {
            return;
        }
        productToBuy = int.Parse(quantityInputFieldAcero.text);
        productToBuy--;
        quantityInputFieldAcero.text = productToBuy.ToString();
        updateCostUI();
    }

    public void checkCanProduce()
    {
        if (FileHandlerStory.Instance.gameData.iron - getNeededIronQuantity(currentProductShown.costIron) >= 0 && int.Parse(quantityInputFieldAcero.text) != 0)
        {
            canProduce = true;
            Color btnColor = produceBtn.GetComponent<Image>().color;
            btnColor.a = 1f;
            produceBtn.GetComponent<Image>().color = btnColor;
        }
        else
        {
            disableBtn();
        }
    }

    private void disableBtn()
    {
        canProduce = false;
        Color btnColor = produceBtn.GetComponent<Image>().color;
        btnColor.a = 0.5f;
        produceBtn.GetComponent<Image>().color = btnColor;
    }

}

[System.Serializable]
public class ProductCostValues
{
    public string title;
    public int costIron;

    public ProductCostValues(string title, float costIron)
    {
        this.title = title;
        this.costIron = Mathf.RoundToInt(costIron);
    }

}
