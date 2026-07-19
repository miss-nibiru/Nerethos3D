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
    [SerializeField] private TargetInfoUI targetInfoUI;
    
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
    private PlayerAttackCommand _pendingPlayerAttackCommand;
    
    private BattleMemory _battleMemory;
    private bool _battleIsOver;
    
    public StateMachine StateMachine => stateMachine;
    public PlayerTurnController PlayerTurnController => playerTurnController;
    public PlayerCombatantController PlayerController => playerController;
    public EnemyController EnemyController => enemyController;
    public BattleUIManager BattleUIManager => battleUIManager;
    public BattleInputManager BattleInputManager => inputManager;
    public PlayerAttackCommand PendingPlayerAttackCommand => _pendingPlayerAttackCommand;
    public bool BattleIsOver => _battleIsOver;
    public BattleMemory BattleMemory => _battleMemory;
    public TargetInfoUI TargetInfoUI => targetInfoUI;
    
    //STATES CONSTRUCTORS
    public PlayerActionSelectState PlayerActionSelectState => _playerActionSelectState;
    public PlayerTargetSelectState PlayerTargetSelectState => _playerTargetSelectState;
    public PlayerPatternState PlayerPatternState => _playerPatternState;
    public PlayerAttackResolutionState AttackResolutionState => _attackResolutionState;
    public EnemyTurnState EnemyTurnState => _enemyTurnState;
    

    private void Start()
    {
        _battleMemory = new BattleMemory();
        
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
    
    public int GetCurrentPatternLength()
    {
        return playerTurnController.CurrentWeapon.MaxPatternLength;
    }
    
    
    public void ReceivedPattern(List<PlayerAttackData.InputActionType> submittedPattern)
    {
        
        WeaponData currentWeapon = playerTurnController.CurrentWeapon;
        if (_battleIsOver) return;
        
        PlayerAttackData resolvedAction = currentWeapon.ResolveAction(submittedPattern);
        Debug.Log("Resolved Attack: " + resolvedAction.AttackName + " Damage: " + resolvedAction.BaseDamage);
        
        if (resolvedAction.RequiresPreviousAttack)
        {
            
            bool hasRequiredPreviousAttack = BattleMemory.WasLastPlayerAction(resolvedAction.RequiredPreviousAttack);
            if (!hasRequiredPreviousAttack)
            {
                Debug.Log(resolvedAction.AttackName + " requires " + resolvedAction.RequiredPreviousAttack.AttackName + " first. Using basic attack instead.");
                resolvedAction = currentWeapon.BasicAttackPattern;
            }
            
        }

        if (resolvedAction.AttackStrategy == null) //connected to attack strategies
        {
            Debug.LogError(resolvedAction.AttackName + " has no attack strategy assigned.");
            return;
        }

        // Creates the command -- PlayerAttackResolutionState executes
        PlayerAttackCommand playerAttackCommand = new PlayerAttackCommand(resolvedAction, currentWeapon, submittedPattern);
        SetPendingPlayerAttackCommand(playerAttackCommand);

        Debug.Log("Pending player attack command set: " + playerAttackCommand.AttackData.AttackName);
        ChangeBattleState(AttackResolutionState);
        
    }
    
    public void SetPendingPlayerAttackCommand(PlayerAttackCommand command)
    {
        _pendingPlayerAttackCommand = command;
    }

    private void ResolvePlayerTurn()
    {
        
        //apply weapon damage
        //check if the point was destroyed and apply the correct damage to the enemy health
        //check if the battle was won
        //start moving into enemy turn 
        
    }

    public void WinBattle()
    {
        
        _battleIsOver = true;
        battleUIManager.ShowVictory();
        
    }

    public void LoseBattle()
    {
        _battleIsOver = true;
        battleUIManager.ShowDefeat();
        
        
    }


}