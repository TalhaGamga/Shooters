using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HouseBase : MonoBehaviour, IDamagable
{
    private float health;
    [SerializeField] private float currentHealth;

     public float spawnInterval;

    [SerializeField] private HouseDataSO houseDataSO;

    public Transform targetHouse;

    [SerializeField] Renderer _renderer;

    public Transform spawnPoint;

    public Coroutine IESpawnCharacterCor;

    [SerializeField] HpBar hpBar;


    public int spawnLevel = 0;
    [SerializeField] int spawnUpgradeFactory = 10;
    public virtual void OnEnable()
    {
        EventManager.OnGameEnd += StopSpawning;
    }

    public virtual void OnDisable()
    {
        EventManager.OnGameEnd -= StopSpawning;
    }

    public virtual void Start()
    {
        health = houseDataSO.health;
        currentHealth = health; 

        spawnInterval = houseDataSO.spawnInterval;

        _renderer.material.color = houseDataSO.color;
    }

    public void Die()
    {
        EventManager.OnGameEnd?.Invoke();

        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        SetHpBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }



    private void StopSpawning()
    {
        StopCoroutine(IESpawnCharacterCor); 
    }

    private void SetHpBar()
    {
        hpBar.SetFillRate(currentHealth / health);
    }

    public virtual void UpgradeSpawn()
    {
            spawnInterval -= UpgradeManager.Instance.CalculateIncreasingValue(spawnUpgradeFactory, 0.02f);
            spawnUpgradeFactory--;
            spawnLevel++;
    }
}
