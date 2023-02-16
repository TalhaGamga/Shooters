using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterBase : MonoBehaviour, IDamagable
{
    [SerializeField] private float health;
    [SerializeField] private float currentHealth;
    [SerializeField] CharacterDataSO charDataSO;

    private void Awake()
    {
        health = charDataSO.health;
        currentHealth = health;
    } 

    public virtual void Die()
    { 
        ResetChar();

        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void ResetChar()
    {
        currentHealth = health;
    }
}
