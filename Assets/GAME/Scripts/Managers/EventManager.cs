using System;
using UnityEngine;

public class EventManager 
{
    public static Action OnGameStarted;

    public static Action OnGameEnd;

    public static Action OnUpgradeSpawnSpeed;

    public static Func<long, string> OnGettingAbbreviation;

    public static Action<ShopItemBase> OnPurchasingShopItem;

    public static Action<float> OnSettingMoney;

    public static Action OnEnemyKilled;

    public static Func<int, bool> OnPlayButtonPressed;
}
