using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  this script is just a small helper - i will have more scripts but this will only handle one single job,
/// getting the list of targets and interact with the selected points
/// </summary>

public class CombatTargetSelector
{
    private readonly List<CombatTargetPointController> _targetPoints;
    private int _selectedIndex;

    public CombatTargetPointController SelectedTargetPoint
    {
        get
        {
            if (_targetPoints == null || _targetPoints.Count == 0) return null;
            return _targetPoints[_selectedIndex];
        }
    }

    public CombatTargetSelector(List<CombatTargetPointController> targetPoints)
    {
        _targetPoints = targetPoints;
        _selectedIndex = 0;
    }

    public void SelectPoint(int direction)
    {
        if (_targetPoints == null || _targetPoints.Count == 0) return;
        SelectedTargetPoint.SetBaseVisual();

        _selectedIndex = GetNextTargetableIndex(direction);
        SelectedTargetPoint.SetSelectedVisual();
        Debug.Log("Selected target point: " + SelectedTargetPoint.PointData.TargetPointName);
    }
    
    private bool IsTargetableIndex(int index) // we need to say the core is not targeteable when its hidden
    {
        return _targetPoints[index].CanBeTargeted;
    }
    
    private int GetNextTargetableIndex(int direction)
    {
        int checkedPoints = 0;
        int nextIndex = _selectedIndex;

        while (checkedPoints < _targetPoints.Count)
        {
            nextIndex = GetWrappedIndex(nextIndex + direction);
            if (IsTargetableIndex(nextIndex)) return nextIndex; checkedPoints++;
        }

        return _selectedIndex;
    }

    private int GetWrappedIndex(int index)
    {
        if (index < 0) return _targetPoints.Count - 1;
        if (index >= _targetPoints.Count) return 0;
        return index;
    }
}
