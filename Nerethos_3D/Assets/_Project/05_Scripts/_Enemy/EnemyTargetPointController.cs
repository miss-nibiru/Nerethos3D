using UnityEngine;
/// <summary>
/// this controls each independendt body part and knows their own shortcomings and such
/// also controls the visuals of the target points in the future - for now has a hover
/// </summary>

public class EnemyTargetPointController : MonoBehaviour
{
    [SerializeField] private EnemyTargetPointData pointData;

    private int _currentHealth;
    private bool _isBroken;
    private EnemyPointFeedback _pointFeedback;

    public EnemyTargetPointData PointData => pointData;
    public int CurrentHealth => _currentHealth;
    public bool IsBroken => _isBroken;

    private void Awake()
    {
        _pointFeedback = GetComponent<EnemyPointFeedback>();
    }

    public void Initialize(EnemyTargetPointData data)
    {
        
        pointData = data;

        if (!pointData) return;
        _currentHealth = pointData.MaxHealth;
        _isBroken = false;

        SetBaseVisual();
        Debug.Log(pointData.TargetPointName + " initialized with HP: " + _currentHealth + " / MaxHealth from data: " + pointData.MaxHealth);
        
    }

    public int TakeDamage(int rawDamageAmount) // when player attacks a target point, it also affects main health bar
    {
        if (_isBroken) return 0;
       
        int finalDamage = CalculateDamage(rawDamageAmount);
        
        
        Debug.Log(
            pointData.TargetPointName +
            " damage check | Raw: " +
            rawDamageAmount +
            " | Multiplier: " +
            pointData.DamageMultiplier +
            " | Final: " +
            finalDamage +
            " | HP before: " +
            _currentHealth
        );
        
        
        int healthBeforeDamage = _currentHealth;

        _currentHealth -= finalDamage;

        if (_currentHealth <= 0) _currentHealth = 0;
        if (_currentHealth == 0) BreakTargetPoint();
        
        int actualDamageTaken = healthBeforeDamage - _currentHealth;
        
        Debug.Log(
            pointData.TargetPointName +
            " HP after damage: " +
            _currentHealth +
            " | IsBroken: " +
            _isBroken
        );
        
        return actualDamageTaken;
        
    }

    private int CalculateDamage(int rawDamageAmount)
    {
        float multiplier = pointData.DamageMultiplier;

        // Temporary
        if (multiplier <= 0) multiplier = 1f;
        return Mathf.RoundToInt(rawDamageAmount * multiplier); //To round up decimals for the damage multiplier 
    }

    private void BreakTargetPoint()
    {
        _isBroken = true;
        Debug.Log(pointData.TargetPointName + " has been broken.");

        // To be implemented triggering debuffs, buffs, spawning of the boss cores, special things
    }

    public void SetBaseVisual()
    {
        if (_pointFeedback != null) _pointFeedback.SetBase();
    }

    public void SetSelectedVisual()
    {
        if (_pointFeedback != null) _pointFeedback.SetSelected();
    }
}