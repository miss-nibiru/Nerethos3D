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
    
    [SerializeField] private string attackName;
    [SerializeField] private List<InputActionType> inputPatterns;
    [SerializeField] private int baseDamage;
    
    public string AttackName => attackName;
    public List<InputActionType> InputPatterns => inputPatterns;
    public int BaseDamage => baseDamage;
}