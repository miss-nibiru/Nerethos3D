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
        //BattleManager.ChangeBattleState(BattleManager.PlayerActionSelectState);
        
        
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
