using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private float totalMoney;
    [SerializeField] private float passiveIncomeMoney;
    private float incomeFactory = 1;

    bool isGameStarted = false;
    private void OnEnable()
    {
        EventManager.OnPurchasingShopItem += Purchase;  
        EventManager.OnSettingMoney += SetMoney;
        EventManager.OnGameEnd += StopPassiveIncome;

        EventManager.OnGameStarted += StartPassiveIncome;

    }

    private void OnDisable()
    {
        EventManager.OnPurchasingShopItem -= Purchase;
        EventManager.OnSettingMoney -= SetMoney;
        EventManager.OnGameEnd -= StopPassiveIncome;

        EventManager.OnGameStarted -= StartPassiveIncome;
    }

    private void Update()
    {
        if (isGameStarted)
        {

            DoMoneyHack();

            EventManager.OnSettingMoney.Invoke((passiveIncomeMoney * incomeFactory) * Time.deltaTime);
        }
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

    private void StopPassiveIncome()
    {
        GetComponent<MoneyManager>().enabled = false;
    }

    private void StartPassiveIncome()
    {
        isGameStarted = true;
    }
}
