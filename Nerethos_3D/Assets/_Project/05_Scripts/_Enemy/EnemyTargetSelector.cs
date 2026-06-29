using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  this script is just a small helper - i will have more scripts but this will only handle one single job,
/// getting the list of targets and interact with the selected points
/// </summary>

public class EnemyTargetSelector
{
    private readonly List<EnemyTargetPointController> _targetPoints;
    private int _selectedIndex;

    public EnemyTargetPointController SelectedTargetPoint
    {
        get
        {
            if (_targetPoints == null || _targetPoints.Count == 0) return null;
            return _targetPoints[_selectedIndex];
        }
    }

    public EnemyTargetSelector(List<EnemyTargetPointController> targetPoints)
    {
        _targetPoints = targetPoints;
        _selectedIndex = 0;
    }

    public void SelectPoint(int direction)
    {
        if (_targetPoints == null || _targetPoints.Count == 0) return;
        SelectedTargetPoint.SetBaseVisual();

        _selectedIndex = GetWrappedIndex(_selectedIndex + direction);
        SelectedTargetPoint.SetSelectedVisual();
        Debug.Log("Selected target point: " + SelectedTargetPoint.PointData.TargetPointName);
    }

    private int GetWrappedIndex(int index)
    {
        if (index < 0) return _targetPoints.Count - 1;
        if (index >= _targetPoints.Count) return 0;
        return index;
    }
}
