using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  this script is just a small helper - i will have more scripts but this will only handle one single job,
/// getting the list of targets and interact with the selected points
/// this can connect to all posisble targets in game so in the future if facey has a companion, enemy can target through this first
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
    
    public CombatTargetPointController GetRandomTargetablePoint()
    {
        if (_targetPoints == null || _targetPoints.Count == 0) return null;
        List<CombatTargetPointController> targetablePoints = new();

        foreach (CombatTargetPointController targetPoint in _targetPoints)
        {
            if (targetPoint.CanBeTargeted) targetablePoints.Add(targetPoint);
        }

        if (targetablePoints.Count == 0) return null;
        int randomIndex = Random.Range(0, targetablePoints.Count);
        return targetablePoints[randomIndex];
    }

    public CombatTargetPointController SelectRandomTargetablePoint()
    {
        CombatTargetPointController randomTargetPoint = GetRandomTargetablePoint();

        if (randomTargetPoint == null) return null;
        if (SelectedTargetPoint != null) SelectedTargetPoint.SetBaseVisual();
        _selectedIndex = _targetPoints.IndexOf(randomTargetPoint);

        SelectedTargetPoint.SetSelectedVisual();
        Debug.Log("Randomly selected target point: " + SelectedTargetPoint.PointData.TargetPointName);
        return SelectedTargetPoint;
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
