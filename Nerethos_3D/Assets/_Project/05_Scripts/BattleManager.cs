using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the entirety of the battle and the flow. Starts, player turn, check for patterns, apply attack results, enemy turn, check win/lose conditions, checks for damage dealt
/// Battle result (death or capture)
/// </summary>
public class BattleManager : MonoBehaviour
{
    
    [SerializeField] private PlayerTurnController playerTurnController;
    [SerializeField] private EnemyController enemyController;

    private bool _battleIsOver;
    
    private enum TurnActionType
    {
        //WeaponSelection,
        ActionSelection,
        PatternInput,
        TurnResolution,
        
    }

    private TurnActionType _turnActionType;
    
    
    private void Start()
    {
        SetTurnActionType(TurnActionType.ActionSelection); // start the battle with the player selecting an action to perform
        _battleIsOver = false;
        
        Debug.Log("Battle started! Player's turn. Select an action to perform.");
        
    }
    
    private void SetTurnActionType(TurnActionType newType)
    {
        _turnActionType = newType;
        Debug.Log("Turn action type changed to: " + _turnActionType);
    }
    
    
    
    public int GetCurrentPatternLength()
    {
        return playerTurnController.CurrentWeapon.MaxPatternLength;
    }
    
    public void ReceivedPattern(List<WeaponAttackData.InputActionType> submittedPattern)
    {
        WeaponData currentWeapon = playerTurnController.CurrentWeapon;

        if (_battleIsOver)
        {
            Debug.Log("Battle is already over!");
            return;
        }
        
        WeaponAttackData resolvedAction = currentWeapon.ResolveAction(submittedPattern);

        Debug.Log("Resolved Attack: " + resolvedAction.AttackName + " Damage: " + resolvedAction.BaseDamage);
        
        
        if (resolvedAction.ActionType == WeaponAttackData.WeaponActionType.Offensive)
        {
            enemyController.TakeDamageOnPoint(resolvedAction.BaseDamage);
            Debug.Log("Dealt " + resolvedAction.BaseDamage + " damage to target point!");
            
            
            if (enemyController.IsDead)
            {
                WinBattle();
            }
            
            
        }
        else if (resolvedAction.ActionType == WeaponAttackData.WeaponActionType.Defensive)
        {
            // apply defense buff to player
            Debug.Log("Applying defense buff to player!");
        }
        else if (resolvedAction.ActionType == WeaponAttackData.WeaponActionType.Overdrive)
        {
            //to use the overdrive the player needs to have created a correct chain of inputs and actions
            Debug.Log("Activating overdrive mode for player!");
        }
        
    }

    private void WinBattle()
    {
        
        _battleIsOver = true;
        Debug.Log("Player has won the battle!");
        
    }
    
}