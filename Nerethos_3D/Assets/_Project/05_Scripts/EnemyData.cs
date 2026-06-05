using UnityEngine;

/// <summary>
///  this scriptable object controls all the smaller mobs around, not bosses
/// this can be smaller mobs in dungeons, humans, creatures, etc
/// </summary>

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
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
    
    
    
}
