using UnityEngine;

[CreateAssetMenu(fileName = "OffensiveAttackStrategy", menuName = "Scriptable Objects/Strategies/Offensive")]
public class OffensiveAttackStrategy : PlayerAttackStrategy
{
    
    public override void Execute(PlayerAttackData attackData, BattleManager battleManager)
    {
        battleManager.EnemyController.DamageSelectedTarget(attackData.BaseDamage);
        Debug.Log(attackData.AttackName + " dealt " + attackData.BaseDamage + " damage to target point!");
        if (battleManager.EnemyController.IsDead) battleManager.WinBattle();
        
    }
    
}