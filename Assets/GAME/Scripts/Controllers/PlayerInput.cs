using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera camera;

    [SerializeField] private RectTransform selectionBox;

    private Vector2 startMousePosition;

    private void Update()
    {
        HandleSelectionInputs();
    }

    private void HandleSelectionInputs()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            selectionBox.sizeDelta = Vector2.zero;
            selectionBox.gameObject.SetActive(true);
            startMousePosition = Input.mousePosition;
        }

        else if (Input.GetKey(KeyCode.Mouse0))
        {
            ResizeSelectionBox();
        }

        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            selectionBox.sizeDelta = Vector2.zero;
            selectionBox.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && SelectionManager.Instance.selectedUnits.Count > 0)
        {
            int interval = 0;

            foreach (HybridMovement hybridMovement in SelectionManager.Instance.selectedUnits)
            {
                if (hybridMovement)
                {
                    hybridMovement.SwitchMovement(interval++);
                }
            }
        }
    } 

    private void ResizeSelectionBox()
    {
        float width = Input.mousePosition.x - startMousePosition.x;
        float height = Input.mousePosition.y - startMousePosition.y;

        selectionBox.anchoredPosition = startMousePosition + new Vector2(width / 2, height / 2);
        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

        Bounds bounds = new Bounds(selectionBox.anchoredPosition, selectionBox.sizeDelta);

        for (int i = 0; i < SelectionManager.Instance.availableUnits.Count; i++)
        {
            if (UnitIsInSelectionBox(camera.WorldToScreenPoint(SelectionManager.Instance.availableUnits[i].transform.position), bounds))
            {
                SelectionManager.Instance.Select(SelectionManager.Instance.availableUnits[i]);
            }

            else
            {
                SelectionManager.Instance.Deselect(SelectionManager.Instance.availableUnits[i]);
            }
        }
    }

    private bool UnitIsInSelectionBox(Vector2 position, Bounds bounds)
    {
        return position.x > bounds.min.x && position.x < bounds.max.x
            && position.y > bounds.min.y && position.y < bounds.max.y; 
    }
}
