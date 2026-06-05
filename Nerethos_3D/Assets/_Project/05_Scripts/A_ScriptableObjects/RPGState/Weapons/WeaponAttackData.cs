using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponAttackData", menuName = "Scriptable Objects/Weapons/Weapon Attack Data")]
public class WeaponAttackData : ScriptableObject
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
    
    [SerializeField] private string attackName;
    [SerializeField] private List<InputActionType> inputPatterns;
    [SerializeField] private WeaponActionType actionType;
    
    [SerializeField] private int baseDamage;
    
    public string AttackName => attackName;
    public List<InputActionType> InputPatterns => inputPatterns;
    public WeaponActionType ActionType => actionType;
    public int BaseDamage => baseDamage;
}