using System.Collections;
using UnityEngine;

public class PlayerAttackAnnouncementState : IBattleState
{
    private BattleManager _battleManager;
    private Coroutine _announcementCoroutine;

    public void Initialize(BattleManager battleManager)
    {
        _battleManager = battleManager;
    }

    public void EnterState()
    {
        PlayerAttackCommand command =
            _battleManager.PendingPlayerAttackCommand;

        if (command == null)
        {
            _battleManager.ChangeBattleState(_battleManager.AttackResolutionState);
            return;
        }

        _battleManager.BattleUIManager.ShowAttackAnnouncement(command.AttackData.AttackName);
        _announcementCoroutine = _battleManager.StartCoroutine(WaitForAnnouncement());
    }

    private IEnumerator WaitForAnnouncement()
    {
        yield return new WaitForSecondsRealtime(1f);

        _battleManager.BattleUIManager.HideAttackAnnouncement();
        _announcementCoroutine = null;
        _battleManager.ChangeBattleState(_battleManager.AttackResolutionState);
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
        //
    }

    public void PerformState()
    {
        //
    }

    public void MoveState(int direction)
    {
        //
    }
}