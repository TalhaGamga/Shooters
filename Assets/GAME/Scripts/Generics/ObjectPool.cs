using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T prefab;
    [SerializeField] private int poolSize;

    [SerializeField] private Transform parent;

    [SerializeField] private List<T> pool = new List<T>();

    public static Func<T> OnGettingFromPool;

    private void OnEnable()
    {
        OnGettingFromPool += GetFromPool;
    }

    private void OnDisable()
    {
        OnGettingFromPool -= GetFromPool;
    }

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            T obj = Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            pool.Add(obj);
        }
    }

    public T GetFromPool()
    {
        foreach (T obj in pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        T newObj = Instantiate(prefab, transform);
        pool.Add(newObj);
        return newObj;
    }
}