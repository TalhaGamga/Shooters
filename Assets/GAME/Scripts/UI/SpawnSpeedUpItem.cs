using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpeedUpItem : ShopItemBase
{
    public override void DoOperation()
    {
        EventManager.OnUpgradeSpawnSpeed?.Invoke();
    }
}
