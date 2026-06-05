using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// Enemy has multiple target points.
    /// Each target point can be destroyed.
    /// Destroyed points can affect rewards / drops / battle options later.
    /// Weakspot is not necessarily instant-kill.
    /// Weakspot gives better drop chance if destroyed.
    /// </summary>
    
    //All stats that change during battle have to be here from enemy data
    
    private int _enemyAttackPower;
    private int _enemyDefensePower;
    private int _enemySpeed;
    private bool _canTakeDamage;
    private bool _isDead;
    
    [SerializeField] private int enemyCurrentHealth;
    [SerializeField] private EnemyData enemyData;
    
    public bool IsDead => _isDead;
    
    private void Start()
    {
        if (enemyData == null)
        {
            Debug.LogError("EnemyData is not assigned for " + gameObject.name + "Assign now." );
            return;

        }
        
        enemyCurrentHealth = enemyData.EnemyMaxHealth;
        _enemyAttackPower = enemyData.EnemyAttackPower;
        _enemyDefensePower = enemyData.EnemyDefensePower;
        _enemySpeed = enemyData.EnemySpeed;
        
        _canTakeDamage = true;
        _isDead = false;
        
        Debug.Log("All stats initialized: " + gameObject.name + " Health: " + enemyCurrentHealth + " Attack: " + _enemyAttackPower + " Defense: " + _enemyDefensePower + " Speed: " + _enemySpeed);
        
        
    }

    public void TakeDamage(int damageAmount)
    {
        if (!_canTakeDamage || _isDead)
        {
            return;
        }  
        
        enemyCurrentHealth -= damageAmount;
        Debug.Log(gameObject.name + " has been damaged! current health: " + enemyCurrentHealth);
        
        if (enemyCurrentHealth <= 0)
        {
            
            enemyCurrentHealth = 0;
            _isDead = true;
            _canTakeDamage = false;
            
            Debug.Log(gameObject.name + " has been defeated!");

            //DeadAnimation();
            
        }
        
        Debug.Log(gameObject.name + " took " + damageAmount + " damage! Current health: " + enemyCurrentHealth);
        
    }

    //During battle the enemy can also get status effects added to them that can affect attack, defense and speed (stunning shuts spped to 0)


    // public Animation DeadAnimation();
    // {
    //     // play death animation
    //     return null;
    // }
    //
    
}
