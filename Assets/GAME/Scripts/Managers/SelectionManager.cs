using System.Collections.Generic;
using UnityEngine;
public class SelectionManager : Singleton<SelectionManager>
{   
    public HashSet<HybridMovement> selectedUnits = new HashSet<HybridMovement>();
    public List<HybridMovement> availableUnits = new List<HybridMovement>();

    public void AddToList(HybridMovement hybridMovement)
    {
        availableUnits.Add(hybridMovement);
    }

    public void RemoveFromList(HybridMovement hybridMovement)
    {
        availableUnits.Remove(hybridMovement);
        selectedUnits.Remove(hybridMovement);
    }

    public void Select(HybridMovement unit)
    {
        selectedUnits.Add(unit);
        unit.OnSelected();
    }

    public void Deselect(HybridMovement unit)
    {
        selectedUnits.Remove(unit);
        unit.OnDeselect();
    }

    public void DeselectAll()
    {
        foreach (HybridMovement unit in selectedUnits)
        {
            unit.OnDeselect();
        }

        selectedUnits.Clear();
    }
    
    public bool IsSelected(HybridMovement unit)
    {
        return selectedUnits.Contains(unit);
    }
}
