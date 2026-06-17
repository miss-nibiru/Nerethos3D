using UnityEngine;

public class PlayerTargetSelectState : IBattleState
{ 
    private BattleManager _battleManager;
    
    public BattleManager BattleManager => _battleManager;


    public void Initialize(BattleManager battleManager)
    {
        _battleManager = battleManager;
    }
    
    public void EnterState()
    {
        
        Debug.Log("Choose the target you wanna plow");
        
        
    }
    
    public void ConfirmState()
    {
        Debug.Log("Target confirmed. Moving to pattern input.");
        _battleManager.ChangeBattleState(_battleManager.PlayerPatternState);
    
    }
    
    public void PerformState()
    {
       //working on it!
    }


    public void ExitState()
    {
        
        Debug.Log("Exited player target selection > going into pattern stage");
        
    }
    
    public void MoveState(int direction)
    {
        
        _battleManager.EnemyController.SelectPoint(direction);
    
    }

}
