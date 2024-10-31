using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text moneyUI;
    public TMP_Text happinessUI;
    public TMP_Text coqueUI;
    public TMP_Text ironUI;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateMoneyUI(int money)
    {
        moneyUI.text = money.ToString();
    }

    public void UpdateCoqueUI(int coque)
    {
        coqueUI.text = coque.ToString();
    }

    public void UpdateIronUI(int iron)
    {
        ironUI.text = iron.ToString();
    }

    public void UpdateHappinessUI(int happiness)
    {
        happinessUI.text = happiness.ToString();
    }

}
