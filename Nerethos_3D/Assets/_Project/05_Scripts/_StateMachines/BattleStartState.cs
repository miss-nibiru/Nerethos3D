using UnityEngine;

public class BattleStartState : IBattleState
{ 
    private BattleManager _battleManager;
    
    public BattleManager BattleManager => _battleManager;


    public void Initialize(BattleManager battleManager)
    {
        _battleManager = battleManager;
    }
    public void EnterState()
    {
        Debug.Log("Entering Battle start state, things loaded properly");
        _battleManager.ChangeBattleState(_battleManager.PlayerActionSelectState);
        
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
        
        Debug.Log("Exiting Battle start state, things loaded properly type shit");
        
    }

}
