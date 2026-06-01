using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private int weaponLevel;
    [SerializeField] private int maxPatternLength;
    
    public string WeaponName => weaponName;
    public int WeaponLevel => weaponLevel;
    public int MaxPatternLength => maxPatternLength;
    
    
}
