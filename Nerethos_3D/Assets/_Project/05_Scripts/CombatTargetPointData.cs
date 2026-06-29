using UnityEngine;

/// <summary>
///  holds all the necessary information of each target point and how the weak point works on most regular enemies, nonboss enemies
/// </summary>

[CreateAssetMenu(fileName = "EnemyTargetPointData", menuName = "Scriptable Objects/EnemyTargetPointData")] // imporant to add on all scriptable objects to be detected as such!

public class CombatTargetPointData : ScriptableObject

{
    public enum TargetPointLocation
    {
        Head,
        Torso,
        Abdomen,
        LeftLeg,
        RightLeg,
        Chest,
        Back,
    }

    public enum AlchemyWeakness
    {
        Acid,
        Ember,
        Aether
    }

    public enum StatusEffectType
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
    
    [Header("Target")]
    [SerializeField] private string targetPointName;
    [SerializeField] private TargetPointLocation targetPointLocation;
    
    [Header("Body Role")]
    [SerializeField] private bool isCore;
    [SerializeField] private bool startsHidden;
    [SerializeField] private bool requiredDeadForCoreExposure = true;
    [SerializeField] private bool canBeTargetedWhenBroken;
    
    [Header("HP")]
    [SerializeField] private int targetMaxHealth;
    [SerializeField] private float damageMultiplier;
    
    [Header("Weakness")]
    [SerializeField] private AlchemyWeakness[] alchemyWeakness;
    [SerializeField] private StatusEffectType[] weakStatusEffect;
    [SerializeField] private StatusEffectType[] resistStatusEffect;
    
    [Header("Breaking Behaviour")]
    [SerializeField] private bool canBeBroken; // if this point is a weak point, it can give better rewards when destroyed, but it does not have to be an instant kill
    
    
    public string TargetPointName => targetPointName;
    public TargetPointLocation PointLocation => targetPointLocation;
    public int MaxHealth => targetMaxHealth;
    public float DamageMultiplier => damageMultiplier;
    public AlchemyWeakness[] Weakness => alchemyWeakness;
    public StatusEffectType[] StatusEffects => weakStatusEffect;
    public StatusEffectType[] ResistStatusEffects => resistStatusEffect;
    public bool CanBeBroken => canBeBroken;
    public bool IsCore => isCore;
    public bool StartsHidden => startsHidden;
    public bool RequiredForCoreExposure => requiredDeadForCoreExposure;
    public bool CanBeTargetedWhenBroken => canBeTargetedWhenBroken;
    
    
    
}
