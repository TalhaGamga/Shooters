using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Aim to target

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private Collider[] targetColliders;

    int targetLen;

    public Transform targetHouse;

    [SerializeField] Transform target;

    [SerializeField] float scanRadius = 6;

    [SerializeField] float aimRange = 6;

    [SerializeField] LayerMask scanLayer;

    public Action<Transform> OnSettingTarget;

    [SerializeField] float fireTimer = .5f;

    [SerializeField] Transform firePoint;

    public Action<Vector3, Transform> OnEnableFire;

    float currentFireTimer;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }

    private void Awake()
    {
        targetColliders = new Collider[50];

        currentFireTimer = fireTimer;
    }

    void Update()
    {
        SetTarget();
    }

    private void SetTarget()
    {
        targetLen = Physics.OverlapSphereNonAlloc(transform.position, scanRadius, targetColliders, scanLayer);

        if (targetLen > 0)
        {
            target = targetColliders[0].transform;

            for (int i = 0; i < targetLen; i++)
            {
                Collider tempCollider = targetColliders[i];

                if (Vector3.Distance(transform.position, target.position) > Vector3.Distance(transform.position, tempCollider.transform.position))
                {
                    target = tempCollider.transform;
                }
            }
        }

        else
        {
            target = targetHouse;
        }

        if (target && Vector3.Distance(transform.position, target.position) < aimRange)
        {
            Vector3 targetDir = (target.position - transform.position).normalized;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), 500 * Time.deltaTime);

            currentFireTimer -= Time.deltaTime;

            if (currentFireTimer < 0)
            {
                OnEnableFire?.Invoke(firePoint.position, target);

                currentFireTimer = fireTimer;
            }
        }

        OnSettingTarget?.Invoke(target);
    }

    public void InitTargetDetector(Transform targetHouse)
    {
        this.targetHouse = targetHouse;

        this.target = targetHouse;
    }

}
