using System.Collections.Generic;

public class PlayerAttackCommand
{
    
    private readonly List<PlayerAttackData.InputActionType> _submittedPattern;

    public PlayerAttackData AttackData { get; }
    public WeaponData WeaponData { get; }
    public IReadOnlyList<PlayerAttackData.InputActionType> SubmittedPattern => _submittedPattern;

    public PlayerAttackCommand(PlayerAttackData attackData, WeaponData weaponData, List<PlayerAttackData.InputActionType> submittedPattern)
    {
        AttackData = attackData;
        WeaponData = weaponData;
        _submittedPattern = new List<PlayerAttackData.InputActionType>(submittedPattern);
    }

    public void Execute(BattleManager battleManager)
    {
        AttackData.AttackStrategy.Execute(AttackData, battleManager);
    }
    
}