using UnityEngine;

/// <summary>
/// the easiest thing to make, can all be this pls?
/// </summary>

public interface IBattleState
{

    void EnterState();
    void PerformState();
    void ExitState();
    void ConfirmState();
    void MoveState(int direction);


}
