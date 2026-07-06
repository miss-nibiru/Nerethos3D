using UnityEngine;

[CreateAssetMenu(fileName = "OverdriveStrategy", menuName = "Scriptable Objects/Strategies/Overdrive")]
public class OverdriveStrategy : PlayerAttackStrategy
{
    public override void Execute(PlayerAttackData attackData, BattleManager battleManager)
    {
        
        Debug.Log(attackData.AttackName + " activated an overdrive");
        battleManager.EnemyController.DamageSelectedTarget(attackData.BaseDamage);

        Debug.Log(attackData.AttackName + " dealt " + attackData.BaseDamage + " overdrive damage!");
        if (battleManager.EnemyController.IsDead) battleManager.WinBattle();
        
        //override rules
        // Only works if overdrive meter is full, consumes meter
        // Can hit multiple target points bool, depending on the overdrive
        // All overdrives require a chain - meter depletes if the next attack in chain is not inputed
        // overdrives will show a skipabble comic-like panels. Timeline can be used for this probably.
        
        
    }
    
}