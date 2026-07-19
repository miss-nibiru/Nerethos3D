using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttackData", menuName = "Scriptable Objects/PlayerAttackData")]
public class PlayerAttackData : ScriptableObject
{
    public enum InputActionType
    {
        Up,
        Down,
        Left,
        Right,
        A,
        S,
        X,
        Z
        
    }
    
    public enum WeaponActionType
    {
        Offensive,
        Defensive,
        Overdrive
    }
    
    [Header("Basic Info")]
    [SerializeField] private string attackName;
    [SerializeField] private List<InputActionType> inputPatterns;
    
    [TextArea(2, 4)]
    [SerializeField] private string description;

    [TextArea(2, 4)]
    [SerializeField] private string effectDescription;
    
    [TextArea(2, 4)]
    [SerializeField] private string alchemyDescription;
    
    [Header("Attack Rules")]
    [SerializeField] private WeaponActionType actionType; // categorize and ui
    [SerializeField] private PlayerAttackStrategy attackStrategy; // exdcute the attack strategy not connected to action type
    
    [Header("Pattern Continuation")]
    [SerializeField] private bool canStartContinuation;
    [SerializeField] private bool requiresPreviousAttack;
    [SerializeField] private PlayerAttackData requiredPreviousAttack;
    
    [Header("Attack Base Stats")]
    [SerializeField] private int baseDamage;
    
    [Header("Discovery")]
    [SerializeField] private bool startsDiscovered;
    
    public string AttackName => attackName;
    public List<InputActionType> InputPatterns => inputPatterns;
    public WeaponActionType ActionType => actionType;
    public PlayerAttackStrategy AttackStrategy => attackStrategy;

    public bool CanStartContinuation => canStartContinuation;
    public bool RequiresPreviousAttack => requiresPreviousAttack;
    public PlayerAttackData RequiredPreviousAttack => requiredPreviousAttack;

    public int BaseDamage => baseDamage;
    public string Description => description;
    public string EffectDescription => effectDescription;
    public string AlchemyDescription => alchemyDescription;
    public bool StartsDiscovered => startsDiscovered;
}