using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private float totalMoney;
    private float passiveIncomeMoney;
    private float incomeFactory = 1;

    private void OnEnable()
    {
        EventManager.OnPurchasingShopItem += Purchase;
        EventManager.OnSettingMoney += SetMoney;
    }

    private void OnDisable()
    {
        EventManager.OnPurchasingShopItem -= Purchase;
        EventManager.OnSettingMoney -= SetMoney;
    }

    private void Update()
    {
        DoMoneyHack();

        EventManager.OnSettingPassiveIncome?.Invoke((passiveIncomeMoney * incomeFactory) * Time.deltaTime);
    }

    private void DoMoneyHack()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            EventManager.OnSettingMoney?.Invoke(+1000);
        }
    }

    private void SetMoney(float money)
    {
        totalMoney += money;
    }

    private void Purchase(ShopItemBase item)
    {
        float cost = item.cost;

        if (totalMoney >= cost)
        {
            EventManager.OnSettingMoney(-cost);

            item.Buy();
        }
    }

    private void SetPassiveIncome(float money)
    {
        totalMoney += money;
    }
}