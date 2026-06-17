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
        _battleManager.BattleUIManager.SetActionSelectionUIActive(true); // ui has to move around functionality too :<
        
        
    }
    
    public void ConfirmState()
    {
        Debug.Log("Confirmed, moving into target selection");
        _battleManager.ChangeBattleState(_battleManager.PlayerTargetSelectState);
    
    }
    
    
    public void PerformState()
    {
       //working on it!
    }


    public void ExitState()
    {
        
        Debug.Log("Exited player action select");
        _battleManager.BattleUIManager.SetActionSelectionUIActive(false);
        
        
    }
    
    public void MoveState(int direction)
    {
        _battleManager.BattleUIManager.MoveActionUI(direction);
    }

}
