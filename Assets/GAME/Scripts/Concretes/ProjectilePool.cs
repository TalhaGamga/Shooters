using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private Projectile prefab;
    [SerializeField] private int poolSize;

    private List<Projectile> pool = new List<Projectile>();

    [SerializeField] Transform parent;

    public static Func<Projectile> OnFiringProjectile;

    private void OnEnable()
    {
        OnFiringProjectile += GetPeojectileFromPool;
    }

    private void OnDisable()
    {
        OnFiringProjectile -= GetPeojectileFromPool;
    }

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Projectile obj = Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            pool.Add(obj);
        }
    }

    public Projectile GetPeojectileFromPool()
    {
        foreach (Projectile obj in pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        Projectile newObj = Instantiate(prefab, transform);
        pool.Add(newObj);
        return newObj;
    }
}