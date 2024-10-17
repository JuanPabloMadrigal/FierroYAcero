using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text moneyUI;
    public TMP_Text happinessUI;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateMoneyUI(int money)
    {
        moneyUI.text = money.ToString();
    }

    public void UpdateHappinessUI(int happiness)
    {
        happinessUI.text = happiness.ToString();
    }

}
