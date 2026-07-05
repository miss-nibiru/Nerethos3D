using UnityEngine;

/// <summary>
/// Alright this is gonna be the hub of ALL kinds of attacks from enemies
/// Will add all the possible needed things but not all would be used for the initial prototype
/// </summary>

[CreateAssetMenu(fileName = "EnemyAttackData", menuName = "Scriptable Objects/EnemyAttackData")]
public class EnemyAttackData : ScriptableObject
{
    public enum MoveType
    {
        Attack,
        Defensive,
        SelfBuff,
        Override
        
    }
    

    [SerializeField] private string attackName;
    [SerializeField] private MoveType moveType;
    
    [SerializeField] private int baseDamage;
    [SerializeField] private float coolDownTime;
    [SerializeField] private float accuracy;
    
    [SerializeField] private bool canTargetPlayer;
    [SerializeField] private bool canTargetSelf;
    
    [SerializeField] private bool canCrit;
    [SerializeField] private float criticalChance;
    [SerializeField] private float criticalMultiplier;

    public string AttackName => attackName;
    public MoveType MoveKind => moveType; 
    public int BaseDamage => baseDamage;
    public float CoolDownTime => coolDownTime;
    public float Accuracy =>  accuracy;

    public bool CanTargetPlayer => canTargetPlayer;
    public bool CanTargetSelf => canTargetSelf;
    
    public bool CanCrit  => canCrit;
    public float CriticalChance =>  criticalChance;
    public float CriticalMultiplier  => criticalMultiplier;

}
