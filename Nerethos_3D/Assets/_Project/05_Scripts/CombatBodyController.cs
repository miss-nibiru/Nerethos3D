using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this is a helper as well
/// controls the entire body of the enemy, knows what is main stats and handles sum of target points
/// Checks if this enemy has been defeated
/// </summary>
public class CombatBodyController
{
    private readonly List<CombatTargetPointController> _targetPoints;
    public int CurrentHealth => GetCurrentHealth();
    public int MaxHealth => GetMaxHealth();
    public bool IsDead => CurrentHealth <= 0;

    public CombatBodyController(List<CombatTargetPointController> targetPoints)
    {
        _targetPoints = targetPoints;
    }
    
    private int GetCurrentHealth()
    {
        int totalHealth = 0;

        foreach (CombatTargetPointController targetPoint in _targetPoints)
            if (!targetPoint.IsHidden) totalHealth += targetPoint.CurrentHealth;

        return totalHealth;
    }

    private int GetMaxHealth()
    {
        int totalMaxHealth = 0;

        foreach (CombatTargetPointController targetPoint in _targetPoints)
            if (!targetPoint.IsHidden) totalMaxHealth += targetPoint.PointData.MaxHealth;

        return totalMaxHealth;
    }
    
    public bool AreRequiredPointsBroken()
    {
        foreach (CombatTargetPointController targetPoint in _targetPoints)
        {
            if (!targetPoint.PointData.RequiredForCoreExposure) continue;
            if (!targetPoint.IsBroken) return false;
        }

        return true;
    }
    
    public void ExposeCorePoints()
    {
        foreach (CombatTargetPointController targetPoint in _targetPoints)
        {
            if (!targetPoint.PointData.IsCore) continue;

            targetPoint.ShowTargetPoint();
        }
    }
    
    public void TryExposeCore()
    {
        if (!AreRequiredPointsBroken()) return;

        ExposeCorePoints();
    }
    
}
