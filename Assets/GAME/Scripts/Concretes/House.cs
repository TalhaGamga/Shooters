using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour, IDamagable
{
    private float health;
    [SerializeField] private float currentHealth;

    [SerializeField] private float spawnInterval;

    [SerializeField] private HouseDataSO houseDataSO;

    [SerializeField] Transform targetHouse;

    [SerializeField] Renderer _renderer;

    [SerializeField] private GameObject charPrefab;

    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform spawnParent;

    Coroutine IESpawnCharacterCor;

    [SerializeField] HpBar hpBar;

    private void OnEnable()
    {
        EventManager.OnGameEnd += StopSpawning;
    }

    private void OnDisable()
    {
        EventManager.OnGameEnd -= StopSpawning;
    }

    private void Start()
    {
        health = houseDataSO.health;
        currentHealth = health;

        spawnInterval = houseDataSO.spawnInterval;

        _renderer.material.color = houseDataSO.color;

        IESpawnCharacterCor = StartCoroutine(IESpawnCharacter());
    }

    public void Die()
    {
        EventManager.OnGameEnd?.Invoke();

        Destroy(gameObject);
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

    private IEnumerator IESpawnCharacter()
    {
        while (true)
        {
            GameObject spawnedChar = Instantiate(charPrefab, spawnPoint.position, Quaternion.identity, spawnParent);

            spawnedChar.GetComponent<TargetDetector>().InitTargetDetector(targetHouse);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void StopSpawning()
    {
        StopCoroutine(IESpawnCharacterCor); if (GameManager.Instance.isGameEnd)
        {
            StopCoroutine(IESpawnCharacterCor);
        }
    }
    private void SetHpBar()
    {
        Debug.Log("SetHpBar");
        hpBar.SetFillRate(currentHealth / health);
    }
}
