using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : MonoBehaviour
{
    public string character;
    public int money;
    public int workerSatisfaction;
    public List<GameObject> buildingsInMap;

    public void AddMoney(int amount)
    {
        money += amount;
        UIManager.Instance.UpdateMoneyUI(money);
    }

    public void SubtractMoney(int amount)
    {
        money -= amount;
        UIManager.Instance.UpdateMoneyUI(money);
    }
}
