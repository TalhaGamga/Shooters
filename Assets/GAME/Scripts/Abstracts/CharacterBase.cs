using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour, IDamagable, IMovable
{

    [SerializeField] private float speed;

    [SerializeField] private float damage;

    [SerializeField] CharDataSO charDataSO;

    private void Awake()
    {
        speed = charDataSO.speed;
        damage = charDataSO.damage;
    }


    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage()
    {
        throw new System.NotImplementedException();
    }
}
