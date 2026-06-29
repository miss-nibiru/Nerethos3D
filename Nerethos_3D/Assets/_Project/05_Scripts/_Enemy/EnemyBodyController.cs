using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this is a helper as well
/// controls the entire body of the enemy, knows what is main stats and handles sum of target points
/// Checks if this enemy has been defeated
/// </summary>
public class EnemyBodyController
{
    private readonly List<EnemyTargetPointController> _targetPoints;
    public int CurrentHealth => GetCurrentHealth();
    public int MaxHealth => GetMaxHealth();
    public bool IsDead => CurrentHealth <= 0;

    public EnemyBodyController(List<EnemyTargetPointController> targetPoints)
    {
        _targetPoints = targetPoints;
    }
    
    private int GetCurrentHealth()
    {
        int totalHealth = 0;

        foreach (EnemyTargetPointController targetPoint in _targetPoints)
            totalHealth += targetPoint.CurrentHealth;

        return totalHealth;
    }

    private int GetMaxHealth()
    {
        int totalMaxHealth = 0;

        foreach (EnemyTargetPointController targetPoint in _targetPoints)
            totalMaxHealth += targetPoint.PointData.MaxHealth;

        return totalMaxHealth;
    }
    
}
