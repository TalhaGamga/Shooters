using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HybridMovement : MovementBase, IDirectable
{
    [SerializeField] Vector3 selectedPoint;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask enemyHouseLayer;

    [SerializeField] SpriteRenderer selectionSprite;

    public override void OnEnable()
    {
        base.OnEnable();
        SelectionManager.Instance.AddToList(this);

        navMesh.stoppingDistance = 5;
    }
    public override void OnDisable()
    {
        SelectionManager.Instance.RemoveFromList(this);
        selectionSprite.gameObject.SetActive(false);

        base.OnDisable();
    }

    public void MoveTo()
    {
        navMesh.SetDestination(selectedPoint);
    }

    public void OnDeselect()
    {
        selectionSprite.gameObject.SetActive(false);
    }

    public void OnSelected()
    {
        selectionSprite.gameObject.SetActive(true);
    }

    public void SwitchMovement(float interval)
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            selectedPoint = hit.point;
            navMesh.stoppingDistance = 1f + interval*.5f;
            movement = MoveTo;

            return;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyHouseLayer))
        {
            Debug.Log("EnemyHouse");

            navMesh.stoppingDistance = 5f;
            movement = Move;

            return;
        }
    }
}
