
/// <summary>
/// keeps track of previous player actions.
/// supports continuation attacks
/// </summary>

public class BattleMemory
{
    public PlayerAttackCommand LastPlayerAttackCommand { get; private set; }

    public bool HasLastPlayerAttackCommand => LastPlayerAttackCommand != null;

    public void StorePlayerAttackCommand(PlayerAttackCommand command)
    {
        LastPlayerAttackCommand = command;
    }

    public bool WasLastPlayerAction(PlayerAttackData requiredAction)
    {
        
        if (LastPlayerAttackCommand == null) return false;
        return LastPlayerAttackCommand.AttackData == requiredAction;
        
    }

    public void ClearPlayerAttackCommand()
    {
        LastPlayerAttackCommand = null;
    }
}