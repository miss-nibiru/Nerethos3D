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
    
    public void ReceivedPattern(List<WeaponAttackData.InputActionType> submittedPattern)
    {
        WeaponData currentWeapon = playerTurnController.CurrentWeapon;

        Debug.Log("Current Weapon " + currentWeapon.WeaponName + " Weapon Level " + currentWeapon.WeaponLevel + " Max Pattern Allowed " + currentWeapon.MaxPatternLength);

        WeaponAttackData resolvedAttack = currentWeapon.ResolveAttack(submittedPattern);

        Debug.Log("Resolved Attack: " + resolvedAttack.AttackName + " Damage: " + resolvedAttack.BaseDamage);
    }
    
    // public string BattlePatternResolver(List<WeaponAttackData.InputActionType> submittedPattern)
    // {
    //     // access to the list of patterns in the current weapon to compare with the submitted pattern
    //     
    // }
    
    
    // gets the current weapong from player turn controller
    // // the current weapon resolves the pattern
    // // weapon data returns data the attack had or did
    // // understands what attack it was
    // // applies battle results
    
    
}