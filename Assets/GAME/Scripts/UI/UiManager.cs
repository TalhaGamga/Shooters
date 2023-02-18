using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalMoneyText;

    float totalMoney = 0;

    private void OnEnable()
    {
        EventManager.OnGettingAbbreviation += GetAbbreviation;

        EventManager.OnSettingMoney += SetMoneyText;
    }

    private void OnDisable()
    {
        EventManager.OnGettingAbbreviation -= GetAbbreviation;

        EventManager.OnSettingMoney -= SetMoneyText;
    }

    private void SetMoneyText(float money)
    {
        totalMoney += money;

        totalMoneyText.text = GetAbbreviation((long)totalMoney) + "$";
    }

    public static string GetAbbreviation(long _value)
    {
        if (_value >= 100000000)
        {
            return (_value / 1000000D).ToString("0.#M");
        }
        if (_value >= 1000000)
        {
            return (_value / 1000000D).ToString("0.##M");
        }
        if (_value >= 100000)
        {
            return (_value / 1000D).ToString("0.#k");
        }
        if (_value >= 1000)
        {
            return (_value / 1000D).ToString("0.##k");
        }

        if (_value == 0)
        {
            return "0";
        }

        return _value.ToString("#");
    }

    public void Buy(ShopItemBase item)
    {
        EventManager.OnPurchasingShopItem?.Invoke(item);
    }

}
