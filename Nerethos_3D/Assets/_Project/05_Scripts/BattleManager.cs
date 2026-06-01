using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the entirety of the battle and the flow. Starts, player turn, check for patterns, apply attack results, enemy turn, check win/lose conditions
/// Battle result (death or capture)
/// </summary>
public class BattleManager : MonoBehaviour
{
    
    [SerializeField] private PlayerTurnController playerTurnController;

    
    public int GetCurrentPatternLength()
    {
        return playerTurnController.CurrentWeapon.MaxPatternLength;
    }
    
    public void ReceivedPattern(List<string> submittedPattern)
    {
        
        WeaponData currentWeapon = playerTurnController.CurrentWeapon;
        Debug.Log("Current Weapon " + currentWeapon.WeaponName +  "Weapon Level " + currentWeapon.WeaponLevel +  "Max Pattern Allowed " + currentWeapon.MaxPatternLength);
        
        string resolvedAttack = BattlePatternResolver(submittedPattern);
        
        //temporary damage
        int damage = GetAttackDamage(resolvedAttack);
        
        Debug.Log("Resolved attack: " + resolvedAttack + " Damage: " + damage);
        
    }
    
    
    // these will live here for now until i create the weapon data scripts
    
    private string BattlePatternResolver(List<string> submittedPattern)
    {
        string patternKey = string.Join(",", submittedPattern);

        if (patternKey == "Right,A")
        {
            return "Piercing Slash";
        }
        
        if (patternKey == "Up,Down,S")
        {
            return "Heavy Strike";
        }

        if (patternKey == "Down,Right,Right,A,S")
        {
            return "Dual Wield Strike";
        }

        return "Basic Attack";
        
    }

    private int GetAttackDamage(string attackName)
    {
        if (attackName == "Piercing Slash")
        {
            return 20;
        }
        
        if (attackName == "Heavy Strike")
        {
            return 30;
        }
        
        if (attackName == "Dual Wield Strike")
        {
            return 40;
        }
        
        return 10; // Default damage
        
        
    }
    
    
}
