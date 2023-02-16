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

    public delegate void MovementDel();

    public MovementDel movement;

    public virtual void OnEnable()
    {
        targetDetector.OnSettingTarget += SetTargetAutomatically;

        movement = Move;

        EventManager.OnGameEnd += StopMoving;
    }

    public virtual void OnDisable()
    {
        targetDetector.OnSettingTarget -= SetTargetAutomatically;

        EventManager.OnGameEnd -= StopMoving;
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

    void StopMoving()
    {
        GetComponent<MovementBase>().enabled = false;
    }
}
