using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameModel
{
    public string character;
    public int money;
    public int evento;
    public int empleados;

    public int coque;
    public int coquePrice;
    public int iron;
    public int ironMoneyPrice;
    public int ironPrice;

    public int steel;
    public int steelBar;
    public int steelRail;

    public int steelIronPrice;
    public int steelBarPrice;
    public int steelRailPrice;

    public int salaryAmount;
    public int turnsWithoutSalary;
    public int turnsWithoutEvent;
    public List<BuildingProperties> buildingsList;
    public IronStorehouse ironStorehouse;
    public CokePlant cokePlant;
    public SteelYard steelYard;

    public GameModel(string character, List<BuildingProperties> buildingsList, IronStorehouse ironStorehouse, CokePlant cokePlant, SteelYard steelYard) {
        this.character = character;
        money = 10000;
        evento = -1;
        empleados = 0;
        coque = 0;
        coquePrice = 40;
        iron = 0;
        ironMoneyPrice = 60;
        ironPrice = 10;
        steel = 0;
        steelBar = 0;
        steelRail = 0;
        steelIronPrice = 2200;
        salaryAmount = 10;
        turnsWithoutSalary = 0;
        turnsWithoutEvent = 0;
        this.buildingsList = buildingsList;
        this.ironStorehouse = ironStorehouse;
        this.cokePlant = cokePlant;
        this.steelYard = steelYard;
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

    public void AddSteelBar(int amount)
    {
        steelBar += amount;
    }

    public void AddRail(int amount)
    {
        steelRail += amount;
    }

    public void SetIronMoneyPrice(int price)
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

    public void SetSteelBarPrice(int price)
    {
        steelBarPrice = price;
    }

    public void SetRailPrice(int price)
    {
        steelRailPrice = price;
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

    public void SubtractSteel(int amount)
    {
        steel -= amount;
    }

    public void SubtractSteelBar(int amount)
    {
        steelBar -= amount;
    }

    public void SubtractRail(int amount)
    {
        steelRail -= amount;
    }

}
