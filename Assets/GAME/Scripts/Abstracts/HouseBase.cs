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

    public Transform spawnPoint;

    public Coroutine IESpawnCharacterCor;

    [SerializeField] HpBar hpBar;

    [SerializeField] ParticleSystem dieEffect;

    public int spawnLevel = 0;
    [SerializeField] int spawnUpgradeFactory = 10;
    public virtual void OnEnable()
    {
        EventManager.OnGameEnd += StopSpawning;

        EventManager.OnGameStarted += InitHouse;
    }

    public virtual void OnDisable()
    {
        EventManager.OnGameEnd -= StopSpawning;

        EventManager.OnGameStarted -= InitHouse;
    }

    public virtual void Start()
    {
        health = houseDataSO.health;
        currentHealth = health; 

        spawnInterval = houseDataSO.spawnInterval;
    }

    public void Die()
    {
        EventManager.OnGameEnd?.Invoke();

        dieEffect.gameObject.transform.SetParent(null);

        dieEffect.Play();
        
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
        if (spawnUpgradeFactory>0)
        {
            spawnInterval -= UpgradeManager.Instance.CalculateIncreasingValue(spawnUpgradeFactory, 0.02f);
            spawnUpgradeFactory--;
            spawnLevel++;
        }
    }

    public abstract IEnumerator IESpawnCharacter();

    public abstract void InitHouse();
}
