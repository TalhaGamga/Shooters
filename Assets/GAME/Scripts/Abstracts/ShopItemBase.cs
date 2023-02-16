using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class ShopItemBase : MonoBehaviour
{
    public float cost;
    [SerializeField] TextMeshProUGUI priceText;

    private float startCost;

    private int priceUpdateLevel = 1;

    [SerializeField] private float increaseFactor;

    private void Awake()
    {
        priceText.text = cost.ToString();

        startCost = cost;
    }

    public void UpdatePrice()
    {
        cost *= 1.1f;

        priceText.text = EventManager.OnGettingAbbreviation?.Invoke((long)cost);
    }

    private void UpdateCurrentCost()
    {
        cost += UpgradeManager.Instance.CalculateIncreasingValue(priceUpdateLevel, increaseFactor);

        priceText.text = EventManager.OnGettingAbbreviation?.Invoke((long)cost);

        priceUpdateLevel++;
    }

    public abstract void DoOperation();

    public void Buy()
    {
        DoOperation();

        UpdateCurrentCost();
    }
}
