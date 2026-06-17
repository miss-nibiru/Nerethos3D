using UnityEngine;

public class PlayerPatternState : IBattleState
{ 
    private BattleManager _battleManager;
    
    public BattleManager BattleManager => _battleManager;


    public void Initialize(BattleManager battleManager)
    {
        _battleManager = battleManager;
    }
    
    public void EnterState()
    {
        
        Debug.Log("Entered pattern stage"); 
        _battleManager.BattleInputManager.EnablePatternInput(); //turn on the pattern enterer thingy
        
    }
    
    public void ConfirmState()
    {
    
    }
    
    public void PerformState()
    {
       //working on it! player performs pattern here so we need to connect to input
    }


    public void ExitState()
    {
        
        Debug.Log("Exited player target selection > going into pattern stage");
        _battleManager.BattleInputManager.DisablePatternInput();
        
    }
    
    public void MoveState(int direction)
    {
        
        
    
    }

}
