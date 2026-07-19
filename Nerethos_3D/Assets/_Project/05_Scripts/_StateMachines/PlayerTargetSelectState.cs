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
        
        BattleManager.TargetInfoUI.ShowTarget(
            BattleManager.EnemyController.SelectedTargetPoint
        );
        
        
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
        BattleManager.TargetInfoUI.Hide();
        
    }
    
    public void MoveState(int direction)
    {
        
        _battleManager.EnemyController.SelectPoint(direction);
        BattleManager.TargetInfoUI.ShowTarget(
            BattleManager.EnemyController.SelectedTargetPoint
        );
    
    }

}
