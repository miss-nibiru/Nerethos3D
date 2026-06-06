using System.Collections.Generic;
using TMPro;
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
    
    public enum TurnActionType
    {
        
        //WeaponSelection,
        ActionSelection,
        PointSelection,
        PatternInput,
        TurnResolution,
        EnemyTurn,
        
    }

    private TurnActionType _turnActionType;
    
    public TurnActionType CurrentTurnActionType => _turnActionType; // This will be used to show the correct UI and to know how to handle the player's input when they submit a pattern.
    
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
    

    public void StartPointSelection()
    {
        
        if (_battleIsOver)
        {
            Debug.Log("Battle is already over, you can't start target selection again!");
        }

        if (_turnActionType != TurnActionType.ActionSelection)
        {
            Debug.Log("Cannot start target selection " + _turnActionType);
            return;
        }
        
        SetTurnActionType(TurnActionType.PointSelection);
        Debug.Log("Target selection started");
        
        
    }
    
    private bool CanSelectPoint()
    {
        
        if (_battleIsOver)
        {
            Debug.Log("Battle is over. Cannot select point.");
            return false;
        }

        if (_turnActionType != TurnActionType.PointSelection)
        {
            Debug.Log("Cannot select point selection, current turn action type is not PointSelection.");
            return false;
        }

        return true;
        
    }
    
    public void SelectPoint(int direction)
    {

        if (!CanSelectPoint())
        {
            return;
        }
        
        enemyController.SelectPoint(direction); // the enemy controller will handle the logic of changing the selected point and showing it to the player
        Debug.Log("Point selected");
        
    }
    
    public void ConfirmPointSelection()
    {
        
        Debug.Log("ConfirmPointSelection called. Current state is: " + _turnActionType);

        if (!CanSelectPoint())
        {
            return;
        }

        SetTurnActionType(TurnActionType.PatternInput);
        Debug.Log("Point selection confirmed!!!!!");
        
    }
    
    public void StartPatternInput()
    {
        if (_battleIsOver)
        {
            Debug.Log("Battle is already over, you can't start pattern input!");
            return;
        }
        
        if (_turnActionType != TurnActionType.ActionSelection)
        {
            Debug.Log("Cannot start pattern input, current turn action type is not ActionSelection.");
            return;
            
        }

        if (_turnActionType == TurnActionType.PointSelection)
        {
            Debug.Log("Cannot start pattern input, currently in PointSelection. Please confirm point selection first.");
            return;
        }
        
        SetTurnActionType(TurnActionType.PatternInput);
        Debug.Log("Player is now inputting pattern for the selected action. Max pattern length: " + GetCurrentPatternLength());
        
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
            Debug.Log("Battle is already over, you can't attack anymore!");
            return;
        }
        
        if (_turnActionType != TurnActionType.PatternInput)
        {
            Debug.Log("Received pattern but current turn action type is not PatternInput, ignoring pattern.");
            return;
            
        }
        
        
        WeaponAttackData resolvedAction = currentWeapon.ResolveAction(submittedPattern);

        Debug.Log("Resolved Attack: " + resolvedAction.AttackName + " Damage: " + resolvedAction.BaseDamage);
        
        if (resolvedAction.ActionType == WeaponAttackData.WeaponActionType.Offensive) // if the action of the player is an offensive action (attack or override)
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
        
        SetTurnActionType(TurnActionType.TurnResolution);
        Debug.Log("Player action resolved. Turn resolution starting");
        
    }

    private void ResolvePlayerTurn()
    {
        
        //apply weapon damage
        //check if the point was destroyed and apply the correct damage to the enemy health
        //check if the battle was won
        //start moving into enemy turn 
        
    }

    private void WinBattle()
    {
        
        _battleIsOver = true;
        Debug.Log("Player has won the battle!");
        
    }

    
    
}