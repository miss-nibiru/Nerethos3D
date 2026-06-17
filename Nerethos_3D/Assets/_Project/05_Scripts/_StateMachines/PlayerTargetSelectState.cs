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
        _battleManager.ChangeBattleState(_battleManager.PlayerActionSelectState);
        
        
    }
    
    public void ConfirmState()
    {
    
    }
    
    public void PerformState()
    {
       //working on it!
    }


    public void ExitState()
    {
        
        Debug.Log("Exited player target selection > going into pattern stage");
        
    }

}
