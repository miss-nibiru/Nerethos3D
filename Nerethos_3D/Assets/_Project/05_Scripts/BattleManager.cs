using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the entirety of the life of this script is changing, mutating, becoming a beacon of chonky management unlike how it is right now
/// I am gonna swtich from switch cases heh to state machines
/// wish me luck, im just typing this because starting is ha-
/// </summary>

public class BattleManager : MonoBehaviour
{
    [SerializeField] private BattleInputManager inputManager;
    [SerializeField] private BattleUIManager battleUIManager;
    [SerializeField] private StateMachine stateMachine;
    
    [Header("Player States")]
    [SerializeField] private PlayerTurnController playerTurnController;
    [SerializeField] private PlayerCombatantController playerController;
    
    [Header("Enemy States")]
    [SerializeField] private EnemyController enemyController;

    //1-BATTLE START
    private BattleStartState _battleStartState;
    
    //2-ACTION SELECTION STATE
    private PlayerActionSelectState _playerActionSelectState;
    
    //3-TARGET SELECTION STATE
    private PlayerTargetSelectState _playerTargetSelectState;
    
    //4-PATTERN INPUT STATE
    private PlayerPatternState _playerPatternState;
    
    //5-ATTACK RESOLUTION STATE
    private PlayerAttackResolutionState _attackResolutionState;
    private EnemyTurnState _enemyTurnState;
    
    public StateMachine StateMachine => stateMachine;
    public PlayerTurnController PlayerTurnController => playerTurnController;
    public PlayerCombatantController PlayerController => playerController;
    public EnemyController EnemyController => enemyController;
    public BattleUIManager BattleUIManager => battleUIManager;
    public BattleInputManager BattleInputManager => inputManager;
    
    //STATES CONSTRUCTORS
    public PlayerActionSelectState PlayerActionSelectState => _playerActionSelectState;
    public PlayerTargetSelectState PlayerTargetSelectState => _playerTargetSelectState;
    public PlayerPatternState PlayerPatternState => _playerPatternState;
    public PlayerAttackResolutionState AttackResolutionState => _attackResolutionState;
    public EnemyTurnState EnemyTurnState => _enemyTurnState;
    
    private bool _battleIsOver;

    private void Start()
    {
        _battleStartState = new BattleStartState();
        _battleStartState.Initialize(this);
        
        _playerActionSelectState = new PlayerActionSelectState();
        _playerActionSelectState.Initialize(this);
        
        _playerTargetSelectState = new PlayerTargetSelectState();
        _playerTargetSelectState.Initialize(this);
        
        _playerPatternState = new PlayerPatternState();
        _playerPatternState.Initialize(this);

        _attackResolutionState = new PlayerAttackResolutionState();
        _attackResolutionState.Initialize(this);
        
        _enemyTurnState = new EnemyTurnState();
        _enemyTurnState.Initialize(this);
        
        ChangeBattleState(_battleStartState);

    }
    
    public void ChangeBattleState(IBattleState newBattleState)
    {
        if (!stateMachine)
        {
            Debug.LogError("The thing that manages everything is missing, dumbass");
            return;
        }
        stateMachine.ChangeState(newBattleState);

    }

    public void ConfirmBattleState()
    {
        Debug.Log("Confirming current battle state: " + stateMachine.CurrentState.GetType().Name);
        stateMachine.CurrentState.ConfirmState();
    }

    public void MoveBattleState(int direction)
    {
        stateMachine.CurrentState.MoveState(direction);
    }
    
    /// <summary>
    /// ignoring everything below for now but no touchy so no breaky
    /// </summary>
    // private void Start()
    // {
    //     SetTurnActionType(TurnActionType.ActionSelection); // start the battle with the player selecting an action to perform
    //     _battleIsOver = false;
    //     
    //     Debug.Log("Battle started! Player's turn. Select an action to perform.");
    //     
    // }
    
    public int GetCurrentPatternLength()
    {
        return playerTurnController.CurrentWeapon.MaxPatternLength;
    }
    
    
    public void ReceivedPattern(List<WeaponAttackData.InputActionType> submittedPattern)
    {
        WeaponData currentWeapon = playerTurnController.CurrentWeapon;

        if (_battleIsOver) return;
        
        WeaponAttackData resolvedAction = currentWeapon.ResolveAction(submittedPattern);

        Debug.Log("Resolved Attack: " + resolvedAction.AttackName + " Damage: " + resolvedAction.BaseDamage);
        
        if (resolvedAction.ActionType == WeaponAttackData.WeaponActionType.Offensive)
        {
            enemyController.DamageSelectedTarget(resolvedAction.BaseDamage);
            Debug.Log("Dealt " + resolvedAction.BaseDamage + " damage to target point!");
    
            if (enemyController.IsDead)
            {
                WinBattle();
                return;
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
        
        Debug.Log("Player action resolved. Moving to attack resolution state.");
        ChangeBattleState(AttackResolutionState);
        
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

    public void LoseBattle()
    {
        _battleIsOver = true;
        Debug.Log("Player has lost the battle!");
        
        //still need to add win and lose wrap up 
    }


}