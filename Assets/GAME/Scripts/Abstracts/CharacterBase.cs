using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterBase : MonoBehaviour, IDamagable
{
    [SerializeField] private float health;

    [SerializeField] CharacterDataSO charDataSO;

    private void Awake()
    {
        health = charDataSO.health;
    } 

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }
}
