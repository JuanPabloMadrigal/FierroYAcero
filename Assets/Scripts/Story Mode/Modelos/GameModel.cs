using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameModel
{
    public string character;
    public int money;

    public int coque;
    public int coquePrice;
    public int iron;
    public int ironPrice;
    public int steel;
    public int steelIronPrice;

    public int workerSatisfaction;
    public int turnsWithoutSalary;
    public int turnsWithoutEvent;
    public List<BuildingProperties> buildingsList;
    public IronStorehouse ironStorehouse;
    public CokePlant cokePlant;
    public SteelYard steelYard;

    public GameModel(string character, int money, int workerSatisfaction, int coque, int coquePrice, int iron, int ironPrice, int steel, int steelIronPrice, List<BuildingProperties> buildingsList) { 
        this.character = character;
        this.money = money;
        this.workerSatisfaction = workerSatisfaction;
        this.coque = coque;
        this.coquePrice = coquePrice;
        this.iron = iron;
        this.ironPrice = ironPrice;
        this.steel = steel;
        this.steelIronPrice = steelIronPrice;
        turnsWithoutSalary = 0;
        turnsWithoutEvent = 0;
        this.buildingsList = buildingsList;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UIManager.Instance.UpdateMoneyUI(money);
    }

    public void AddCoque(int amount)
    {
        coque += amount;
        UIManager.Instance.UpdateCoqueUI(coque);
    }

    public void AddIron(int amount)
    {
        iron += amount;
        UIManager.Instance.UpdateIronUI(iron);
    }

    public void AddSteel(int amount) 
    {
        steel += amount;
    }

    public void SetIronPrice(int price)
    {
        ironPrice = price;
    }

    public void SetCoquePrice(int price)
    {
        coquePrice = price;
    }

    public void SetSteelIronPrice(int price)
    {
        steelIronPrice = price;
    }

    public void SubtractMoney(int amount)
    {
        money -= amount;
        UIManager.Instance.UpdateMoneyUI(money);
    }

    public void SubtractIron(int amount)
    {
        iron -= amount;
        UIManager.Instance.UpdateIronUI(iron);
    }

    public void SubtractCoque(int amount)
    {
        coque -= amount;
        UIManager.Instance.UpdateCoqueUI(coque);
    }

}
