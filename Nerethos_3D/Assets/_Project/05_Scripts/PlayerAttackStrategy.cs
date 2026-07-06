using UnityEngine;

public abstract class PlayerAttackStrategy : ScriptableObject
{
    
    public abstract void Execute(PlayerAttackData attackData, BattleManager battleManager);
    
}