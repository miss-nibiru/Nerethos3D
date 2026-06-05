using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  this scriptable object controls all the smaller mobs around, not bosses
/// this can be smaller mobs in dungeons, humans, creatures, etc
/// </summary>

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public enum EnemyType
    
    {
        
        Regular,
        MiniBoss,
        Boss,
        
    }
    
    [SerializeField] private EnemyType enemyType;
    
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
    
    public EnemyType EnemyTypeValue => enemyType;
    
    public List<EnemyTargetPointData> TargetPoints => targetPoints;

    private void OnValidate()
    {
        if (targetPoints == null)
        {
            return;
        }
        
        //there is different enemy types and the amount of ppints vary dependong on what type of enemy it is and it is a range I think - except for Valerian that his weak spots will be modifying depending on the missions facey completes by capturing the creature (add to bible)

        if (enemyType == EnemyType.Regular && targetPoints.Count > 3)
        {
            Debug.Log(enemyName + "is a normal mob and will only have 1 to 3 target points");
        }

        if (enemyType == EnemyType.MiniBoss && targetPoints.Count < 5)
        {
            Debug.Log(enemyName + "is a mini boss and will only have 5 to 8 target points");
        }
        if (enemyType == EnemyType.Boss && targetPoints.Count > 8)
        {
            Debug.Log(enemyName + "is a boss and will only have 5 to 7 target points");
        }
        
    }
    
}
