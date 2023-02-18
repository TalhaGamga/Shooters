using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterBase : MonoBehaviour, IDamagable
{
    [SerializeField] private float health;
    [SerializeField] private float currentHealth;
    [SerializeField] CharacterDataSO charDataSO;

    [SerializeField] ParticleSystem dieEffect;

    private void OnEnable()
    {
        dieEffect.Stop();
        dieEffect.transform.SetParent(gameObject.transform);
        dieEffect.transform.position = transform.position + Vector3.up * 2;
    }
     
    private void Awake()
    {
        health = charDataSO.health;
        currentHealth = health;
    }

    public virtual void Die()
    {
        dieEffect.gameObject.transform.SetParent(null);

        dieEffect.Play();

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

    public virtual void ResetChar()
    {
        currentHealth = health;
    }
}
