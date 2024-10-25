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
    public int workerSatisfaction;
    public List<BuildingProperties> buildingsList;
    public IronStorehouse ironStorehouse;
    public CokePlant cokePlant;
    public SteelYard steelYard;

    public GameModel(string ch, int mon, int workSat, int coq, int coqP, List<BuildingProperties> bl) { 
        character = ch;
        money = mon;
        workerSatisfaction = workSat;
        coque = coq;
        coquePrice = coqP;
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

    public void SubtractMoney(int amount)
    {
        money -= amount;
        UIManager.Instance.UpdateMoneyUI(money);
    }

}
