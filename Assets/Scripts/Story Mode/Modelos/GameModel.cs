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

    public int workerSatisfaction;
    public List<BuildingProperties> buildingsList;
    public IronStorehouse ironStorehouse;
    public CokePlant cokePlant;
    public SteelYard steelYard;

    public GameModel(string ch, int mon, int workSat, int coq, int coqP, int iro, int ironP, List<BuildingProperties> bl) { 
        character = ch;
        money = mon;
        workerSatisfaction = workSat;
        coque = coq;
        coquePrice = coqP;
        iron = iro;
        ironPrice = ironP;
        buildingsList = bl;
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

    public void SetIronPrice(int price)
    {
        ironPrice = price;
    }

    public void SetCoquePrice(int price)
    {
        coquePrice = price;
    }

    public void SubtractMoney(int amount)
    {
        money -= amount;
        UIManager.Instance.UpdateMoneyUI(money);
    }

}
