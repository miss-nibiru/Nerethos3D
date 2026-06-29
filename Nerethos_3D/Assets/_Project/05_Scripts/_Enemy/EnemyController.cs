using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  this knows who the monster is and pulls the stats and data for battle
/// Needs to simply be that so it doesnt have too many jobs - all other data is handled by other shit
/// </summary>

public class EnemyController : MonoBehaviour
{
    private int _enemyAttackPower;
    private int _enemyDefensePower;
    private int _enemySpeed;

    [SerializeField] private EnemyData enemyData;
    private readonly List<EnemyTargetPointController> _targetPoints = new();
    private EnemyTargetSelector _targetSelector;
    private EnemyBodyController _bodyController; //connection to helper 

    public int CurrentHealth => _bodyController.CurrentHealth;
    public int MaxHealth => _bodyController.MaxHealth;
    public bool IsDead => _bodyController.IsDead;
    public EnemyTargetPointController SelectedTargetPoint => _targetSelector?.SelectedTargetPoint;
    

    private void Start()
    {
        
        _enemyAttackPower = enemyData.EnemyAttackPower;
        _enemyDefensePower = enemyData.EnemyDefensePower;
        _enemySpeed = enemyData.EnemySpeed;

        SpawnTargetPoints();

        _targetSelector = new EnemyTargetSelector(_targetPoints);
        _bodyController = new EnemyBodyController(_targetPoints);

        SelectPoint(0);
        
    }

    private void SpawnTargetPoints()
    {
        _targetPoints.Clear();

        foreach (EnemyData.EnemyTargetPointSetup pointSetup in enemyData.TargetPoints) // for each initialized target, we need to get a position and visual
        {
            GameObject newPointVisual = Instantiate(pointSetup.NormalTargetVisual, transform);

            newPointVisual.transform.localPosition = pointSetup.LocalPosition;
            newPointVisual.transform.localScale = pointSetup.LocalScale;

            EnemyTargetPointController targetPointController = newPointVisual.GetComponent<EnemyTargetPointController>();

            if (targetPointController == null) targetPointController = newPointVisual.AddComponent<EnemyTargetPointController>();
            targetPointController.Initialize(pointSetup.TargetPointData);
            _targetPoints.Add(targetPointController);
            
        }
    }
    
    public void SelectPoint(int direction)
    {
        _targetSelector?.SelectPoint(direction);
    }

    public void TakeDamageOnPoint(int damageAmount)
    {
        if (SelectedTargetPoint == null) return;
        if (IsDead) return;
        
        // i hate fail safes, they are so boring

        string targetPointName = SelectedTargetPoint.PointData.TargetPointName;
        int actualDamageTaken = SelectedTargetPoint.TakeDamage(damageAmount);

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
    
}