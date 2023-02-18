using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBase
{
    [SerializeField] int price;
    public override void Die()
    {

        base.Die();

        EventManager.OnSettingMoney?.Invoke(price);

        EventManager.OnEnemyKilled?.Invoke();
    }
}
