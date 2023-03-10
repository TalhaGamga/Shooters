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

    [SerializeField] Animator anim;
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
        RunAnim();
    }

    public void SetTargetAutomatically(Transform target)
    {
        this.target = target;
    }

    void StopMoving()
    {
        GetComponent<MovementBase>().enabled = false;
    }

    public void RunAnim()
    {
        if ((navMesh.remainingDistance > navMesh.stoppingDistance + 1f))
        {
            anim.SetFloat("Speed", 1);
        }

        else
        {
            anim.SetFloat("Speed", 0);
        }
    }
}
