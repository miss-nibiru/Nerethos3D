using System.Collections;
using UnityEngine;

public class EnemyAttackAnnouncementState : IBattleState
{
    private BattleManager _battleManager;
    private Coroutine _announcementCoroutine;

    public void Initialize(BattleManager battleManager)
    {
        _battleManager = battleManager;
    }

    public void EnterState()
    {
        _battleManager.BattleUIManager.ShowAttackAnnouncement(
            "ENEMY TURN"
        );

        _announcementCoroutine =
            _battleManager.StartCoroutine(RunEnemyTurnIntroduction());
    }

    private IEnumerator RunEnemyTurnIntroduction()
    {
        yield return new WaitForSecondsRealtime(0.7f);

        EnemyAttackData chosenAttack =
            _battleManager.EnemyController.ChooseRandomAttack();

        _battleManager.SetPendingEnemyAttack(chosenAttack);

        string attackName = chosenAttack != null
            ? chosenAttack.AttackName
            : "Enemy Attack";

        _battleManager.BattleUIManager.ShowAttackAnnouncement(
            attackName
        );

        yield return new WaitForSecondsRealtime(1f);

        _battleManager.BattleUIManager.HideAttackAnnouncement();
        _announcementCoroutine = null;

        _battleManager.ChangeBattleState(
            _battleManager.EnemyTurnState
        );
    }

    public void ExitState()
    {
        if (_announcementCoroutine != null)
        {
            _battleManager.StopCoroutine(_announcementCoroutine);
            _announcementCoroutine = null;
        }

        _battleManager.BattleUIManager.HideAttackAnnouncement();
    }

    public void ConfirmState()
    {
        
    }

    public void PerformState()
    {
        
    }

    public void MoveState(int direction)
    {
        
    }
}