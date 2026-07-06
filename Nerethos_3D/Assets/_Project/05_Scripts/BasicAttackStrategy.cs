using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttackStrategy", menuName = "Scriptable Objects/Strategies/Basic Attack")]
public class BasicAttackStrategy : PlayerAttackStrategy
{
    public override void Execute(PlayerAttackData attackData, BattleManager battleManager)
    {
        Debug.Log(attackData.AttackName + " activated as a basic fallback attack.");
        battleManager.EnemyController.DamageSelectedTarget(attackData.BaseDamage);

        Debug.Log(attackData.AttackName + " dealt " + attackData.BaseDamage + " basic damage.");
        if (battleManager.EnemyController.IsDead) battleManager.WinBattle();
        
    }
    
}