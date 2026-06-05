using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  this scriptable object controls all the smaller mobs around, not bosses
/// this can be smaller mobs in dungeons, humans, creatures, etc
/// </summary>

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    
    [SerializeField] private List<EnemyTargetPointData> targetPoints; // reference to each point on the enemy to select and attack
    
    [SerializeField] private string enemyName;
    [SerializeField] private int enemyMaxHealth;
    [SerializeField] private int enemyAttackPower;
    [SerializeField] private int enemyDefensePower;
    [SerializeField] private int enemySpeed;
    
    public string EnemyName => enemyName;
    public int EnemyMaxHealth => enemyMaxHealth;
    public int EnemyAttackPower => enemyAttackPower;
    public int EnemyDefensePower => enemyDefensePower;
    public int EnemySpeed => enemySpeed;
    
    public List<EnemyTargetPointData> TargetPoints => targetPoints;
    
    
    
}
