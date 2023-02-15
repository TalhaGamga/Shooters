using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

//Go To Target
public abstract class MovementBase : MonoBehaviour, IMovable
{

    public NavMeshAgent navMesh;

    public Transform target;

    public TargetDetector targetDetector;

    [SerializeField] Transform targetHouse;

    public delegate void MovementDel();

    public MovementDel movement;

    bool isAbleMove = true;
    public virtual void OnEnable()
    {
        targetDetector.OnSettingTarget += SetTargetAutomatically;
    }

    public virtual void OnDisable()
    {
        targetDetector.OnSettingTarget -= SetTargetAutomatically;
    }

    public virtual void Start()
    {
        movement = Move;
    }

    public virtual void Update()
    {
        movement();
    }

    public void Move()
    {
        if (!target)
        {
            return;
        }

        
        navMesh.SetDestination(target.position);
    }

    public void SetTargetAutomatically(Transform target)
    {
        this.target = target;
    }

    public void InitMovement(Transform targetHouse)
    {
        this.targetHouse = targetHouse;

        this.target = targetHouse;
    }

    void StopMoving()
    {
        isAbleMove = false;
        movement = null;
    }
}
