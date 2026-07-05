using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  this knows who the monster is and pulls the stats and data for battle
/// Needs to simply be that so it doesnt have too many jobs - all other data is handled by other shit
/// </summary>

public class EnemyController : MonoBehaviour
{
    public int attackPower;
    private int _defensePower;
    private int _speed;

    [SerializeField] private EnemyData enemyData;
    private readonly List<CombatTargetPointController> _targetPointControllers = new();
    private CombatTargetSelector _targetSelector;
    private CombatBodyController _bodyController; //connection to helper 

    public int AttackPower => attackPower;
    public int CurrentHealth => _bodyController.CurrentHealth;
    public int MaxHealth => _bodyController.MaxHealth;
    public bool IsDead => _bodyController.IsDead;
    public CombatTargetPointController SelectedTargetPoint => _targetSelector?.SelectedTargetPoint;


    private void Start()
    {

        attackPower = enemyData.EnemyAttackPower;
        _defensePower = enemyData.EnemyDefensePower;
        _speed = enemyData.EnemySpeed;

        SpawnTargetPoints();

        _targetSelector = new CombatTargetSelector(_targetPointControllers);
        _bodyController = new CombatBodyController(_targetPointControllers);

        SelectPoint(0);

    }

    private void SpawnTargetPoints()
    {
        _targetPointControllers.Clear();

        foreach (EnemyData.EnemyTargetPointSetup pointSetup in
                 enemyData.TargetPoints) // for each initialized target, we need to get a position and visual
        {
            GameObject newPointVisual = Instantiate(pointSetup.NormalTargetVisual, transform);

            newPointVisual.transform.localPosition = pointSetup.LocalPosition;
            newPointVisual.transform.localScale = pointSetup.LocalScale;

            CombatTargetPointController targetPointController =
                newPointVisual.GetComponent<CombatTargetPointController>();

            if (targetPointController == null)
                targetPointController = newPointVisual.AddComponent<CombatTargetPointController>();
            targetPointController.Initialize(pointSetup.TargetPointData);
            _targetPointControllers.Add(targetPointController);

        }
    }

    public void SelectPoint(int direction)
    {
        _targetSelector?.SelectPoint(direction);
    }

    public void DamageSelectedTarget(int damageAmount)
    {
        if (SelectedTargetPoint == null) return;
        if (IsDead) return;

        // i hate fail safes, they are so boring

        string targetPointName = SelectedTargetPoint.PointData.TargetPointName;
        int actualDamageTaken = SelectedTargetPoint.TakeDamage(damageAmount);
        _bodyController.TryExposeCore(); // if it can be expose, do so here

        Debug.Log(gameObject.name + " took " + actualDamageTaken + " damage on " + targetPointName);
        Debug.Log(gameObject.name + " total HP: " + CurrentHealth + "/" + MaxHealth);

        if (SelectedTargetPoint.IsBroken)
        {
            Debug.Log(targetPointName + " is broken.");
        }

        if (IsDead)
        {
            Debug.Log(gameObject.name + " has been defeated!");

            // Later: DeadAnimation() and tell BattleManager the battle is won - move to next scene
        }
    }

    public EnemyAttackData ChooseRandomAttack()
    {
        if (!enemyData) return null;
        if (enemyData.AttackPool is null || enemyData.AttackPool.Count == 0) 
        {
            Debug.LogWarning("Add attack pool to the tingamagig");
            return null;
        }

        int randomIndex = Random.Range(0, enemyData.AttackPool.Count);
        EnemyAttackData chosenAttack = enemyData.AttackPool[randomIndex];
        
        Debug.Log(enemyData.EnemyName + "chose the attack: " + chosenAttack.AttackName);

        return chosenAttack;


    }
    
}