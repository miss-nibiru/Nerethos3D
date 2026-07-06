using UnityEngine;

[CreateAssetMenu(fileName = "DefensiveStrategy", menuName = "Scriptable Objects/Strategies/Defensive")]
public class DefensiveStrategy : PlayerAttackStrategy
{
    
    public override void Execute(PlayerAttackData attackData, BattleManager battleManager)
    {
        Debug.Log(attackData.AttackName + " activated a defensive action.");
        // Rules will be added later, now just needs cleanup
    }
    
}