using UnityEngine;

public class PlayerActionSelectState : IBattleState
{ 
    private BattleManager _battleManager;
    
    public BattleManager BattleManager => _battleManager;


    public void Initialize(BattleManager battleManager)
    {
        _battleManager = battleManager;
    }
    
    public void EnterState()
    {
        
        Debug.Log("Player now needs to choose an action");
        BattleManager.ChangeBattleState(BattleManager.PlayerTargetSelectState);
        
        
    }
    
    public void PerformState()
    {
       //working on it!
    }


    public void ExitState()
    {
        
        Debug.Log("Exited player action select");
        
    }

}
