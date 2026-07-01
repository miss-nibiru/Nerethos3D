using UnityEngine;

public class EnemyTurnState : IBattleState
{ 
    private BattleManager _battleManager;
    
    public BattleManager BattleManager => _battleManager;


    public void Initialize(BattleManager battleManager)
    {
        _battleManager = battleManager;
    }
    
    public void EnterState()
    {
        Debug.Log("Starting Enemy turn now");
        
        BattleManager.PlayerController.SelectRandomTargetPoint();//first select the target randomly
        BattleManager.PlayerController.DamageSelectedTarget(BattleManager.EnemyController.AttackPower);
       
        if (BattleManager.PlayerController.IsDead)
        {
            Debug.Log ("player is dead");
            BattleManager.LoseBattle();
            return;
        }
        
        BattleManager.ChangeBattleState(BattleManager.PlayerActionSelectState);

    }
    
    public void ConfirmState()
    {
        //sss
    
    }
    
    public void PerformState()
    {
       //working on it!
    }


    public void ExitState()
    {
        
        Debug.Log("Enemy turn is over now");
        
    }
    
    public void MoveState(int direction)
    {
        
       //
    
    }

}
