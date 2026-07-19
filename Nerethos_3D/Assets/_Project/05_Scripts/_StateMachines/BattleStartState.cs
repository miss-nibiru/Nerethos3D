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
        Debug.Log("Battle is waiting for the tutorial to close.");
    }

    public void ConfirmState()
    {
        if (_battleManager.BattleIsOver) return;
        _battleManager.ChangeBattleState(_battleManager.PlayerActionSelectState);
    }

    public void PerformState()
    {
        //
    }

    public void ExitState()
    {
        Debug.Log("Tutorial closed. Starting player action selection.");
    }

    public void MoveState(int direction)
    {
        //
    }
}