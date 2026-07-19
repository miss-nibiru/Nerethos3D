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
        PlayerAttackCommand playerAttackCommand = BattleManager.PendingPlayerAttackCommand;

        if (playerAttackCommand == null)
        {
            BattleManager.ChangeBattleState(
                BattleManager.EnemyAttackAnnouncementState
            );
            return;
        }

        playerAttackCommand.Execute(BattleManager);
        BattleManager.BattleUIManager.UpdateHealthBars();
        BattleManager.BattleMemory.StorePlayerAttackCommand(playerAttackCommand);
        Debug.Log("Stored last player attack command: " + playerAttackCommand.AttackData.AttackName);

        BattleManager.SetPendingPlayerAttackCommand(null);

        if (BattleManager.BattleIsOver) return;
        BattleManager.ChangeBattleState(
            BattleManager.EnemyAttackAnnouncementState
        );
        
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