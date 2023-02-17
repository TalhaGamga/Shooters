using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HybridMovement : MovementBase, IDirectable
{
    [SerializeField] Vector3 selectedPoint;

    [SerializeField] LayerMask groundLayer;

    [SerializeField] LayerMask enemyLayer;

    [SerializeField] SpriteRenderer selectionSprite;

    float stoppingDistance;

    Coroutine switchAutoMove;

    Vector3 targetDir;
    private void Awake()
    {
        stoppingDistance = navMesh.stoppingDistance;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SelectionManager.Instance.AddToList(this);

        navMesh.stoppingDistance = stoppingDistance;
    }
    public override void OnDisable()
    {
        SelectionManager.Instance.RemoveFromList(this);
        selectionSprite.gameObject.SetActive(false);

        base.OnDisable();
    }

    public void MoveTo()
    {
        targetDir = (selectedPoint - transform.position).normalized;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), 500 * Time.deltaTime);

        navMesh.SetDestination(selectedPoint);

        RunAnim();
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

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
        {
            navMesh.stoppingDistance = stoppingDistance;
            movement = Move;

            return;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            selectedPoint = hit.point;
            navMesh.stoppingDistance = 1f + interval * .5f;
            movement = MoveTo;

            if (switchAutoMove != null)
            {
                StopCoroutine(switchAutoMove);

                switchAutoMove = StartCoroutine(SwitchAutomaticMovement());

                return;
            }

            switchAutoMove = StartCoroutine(SwitchAutomaticMovement());
        }
    }

    private IEnumerator SwitchAutomaticMovement()
    {
        yield return new WaitForSeconds(5f);

        navMesh.stoppingDistance = stoppingDistance;
        movement = Move;

        StopCoroutine(switchAutoMove);

        switchAutoMove = null;
    }
}
