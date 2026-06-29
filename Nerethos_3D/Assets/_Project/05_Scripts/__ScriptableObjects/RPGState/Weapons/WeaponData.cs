using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Holds weapon information. Holds the list of attacks available to the weapon and the max pattern length allowed for the weapon. This will be used by battle manager to know what attacks are available to player
/// and how long the pattern can be for each weapon
/// </summary>


[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private int weaponLevel;
    [SerializeField] private int maxPatternLength;
    
    
    [SerializeField] private WeaponAttackData basicAttackPattern; // this will be used when the player does not match any attack pattern 
    [SerializeField] private List<WeaponAttackData> actions; // this will be used to know what attacks are available to player with this weapon - this will be used by battle manager to know what attacks are available to player
    
    public string WeaponName => weaponName;
    public int WeaponLevel => weaponLevel;
    public int MaxPatternLength => maxPatternLength;
    
    
    //does this weapon have an attack that matches the players input pattern?
    
    public WeaponAttackData ResolveAction(List<WeaponAttackData.InputActionType> submittedPattern)
    {
        // compare the submitted pattern with the list of attack patterns available to the weapon
        // if there is a match, return the attack data for that attack
        // if there is no match, return default attack

        foreach (var attack in actions)
        {
            if (DoPatternsMatch(submittedPattern, attack.InputPatterns))
            {
                return attack;
            }

        }
        
        return basicAttackPattern;
        
    }

    private bool DoPatternsMatch(List<WeaponAttackData.InputActionType> pattern1, List<WeaponAttackData.InputActionType> pattern2)
    {
       //check the lenght of the inputed pattern
       // compare each input by the index it represents always starting at 0 remember!
       
       if (pattern1.Count != pattern2.Count) // if the player did 2 inputs and the attack needs 3, then the attacks cant overlap
       { 
           return false;
           
       }
       
       for (int i = 0; i < pattern1.Count; i++)
       {
           if (pattern1[i] != pattern2[i])
           {
               return false;
           }
       }
       
       return true;
       
    }
    
    
    
}