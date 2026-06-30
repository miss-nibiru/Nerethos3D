using UnityEngine;

public class PlayerAttackResolutionState : IBattleState
{ 
    private BattleManager _battleManager;
    public BattleManager BattleManager => _battleManager;

    public void Initialize(BattleManager battleManager)
    {
        _battleManager = battleManager;
        
    }
    
    public void EnterState()
    {
        Debug.Log("Resolving player attack");
        BattleManager.ChangeBattleState(BattleManager.EnemyTurnState);
    }
    
    public void ConfirmState()
    {
        
        
    }
    
    public void PerformState()
    {
        //
        
    }

    public void ExitState()
    {
        Debug.Log("Exited player attack resolution");
    }
    
    public void MoveState(int direction)
    {
    }
}