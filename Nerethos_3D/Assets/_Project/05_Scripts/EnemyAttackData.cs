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

    public enum DamageEffects
    {
        Corrosion,
        Marked,
        Stunned,
        Bleeding,
        Guarding,
        Berserk,
        Riposte,
        Madness
    }

    [SerializeField] private string attackName;
    [SerializeField] private MoveType moveType;
    [SerializeField] private float attackDamage;
    [SerializeField] private float coolDownTime;
    [SerializeField] private float accuracy;
    
    [SerializeField] private bool canTargetPlayer;
    [SerializeField] private bool canTargetSelf;
    
    [SerializeField] private int baseDamage;
    [SerializeField] private DamageEffects damageEffects;
    [SerializeField] private float procChance;
    [SerializeField] private float damageOverTime;
    [SerializeField] private int effectTurnDuration;

    [SerializeField] private bool canCrit;
    [SerializeField] private float criticalChance;
    [SerializeField] private float criticalDamage;
    [SerializeField] private float criticalOverTime;

    public string AttackName => attackName;
    public MoveType MoveKind => moveType; 
    public float AttackDamage => attackDamage;
    public float CoolDownTime => coolDownTime;
    public float Accuracy =>  accuracy;

    public bool CanTargetPlayer => canTargetPlayer;
    public bool CanTargetSelf => canTargetSelf;
    
    public int BaseDamage => baseDamage;
    public DamageEffects DamageType => damageEffects;
    public float ProcChance => procChance;
    public float DamageOverTime => damageOverTime;
    public int EffectTurnDuration => effectTurnDuration;
    public bool CanCrit  => canCrit;
    public float CriticalChance =>  criticalChance;
    public float CriticalDamage  => criticalDamage;
    public float CriticalOverTime  => criticalOverTime;

}
