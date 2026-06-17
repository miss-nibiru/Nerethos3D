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
        BattleManager.ChangeBattleState(BattleManager.PlayerActionSelectState);
        
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
