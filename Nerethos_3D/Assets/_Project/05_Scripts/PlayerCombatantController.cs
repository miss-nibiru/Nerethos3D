using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls Facey's battle body.
/// Spawns her target points and connects them to the shared combat body system.
/// </summary>
public class PlayerCombatantController : MonoBehaviour
{
    
    [SerializeField] private PlayerCombatantData playerData;

    private readonly List<CombatTargetPointController> _targetPointControllers = new();
    private CombatTargetSelector _targetSelector;
    private CombatBodyController _bodyController; // all things that can take damage use the same helper public class
    
    public int CurrentHealth => _bodyController.CurrentHealth;
    public int MaxHealth => _bodyController.MaxHealth;
    public bool IsDead => _bodyController.IsDead;
    public CombatTargetPointController SelectedTargetPoint => _targetSelector?.SelectedTargetPoint;

    private void Start()
    {
        if (playerData == null) return;

        SpawnTargetPoints();

        _targetSelector = new CombatTargetSelector(_targetPointControllers);
        _bodyController = new CombatBodyController(_targetPointControllers);

        SelectPoint(0);
        
    }

    private void SpawnTargetPoints()
    {
        _targetPointControllers.Clear();

        foreach (PlayerCombatantData.PlayerTargetPointSetup pointSetup in playerData.TargetPoints)
        {
            GameObject newPointVisual = Instantiate(pointSetup.TargetPointVisual, transform);

            newPointVisual.transform.localPosition = pointSetup.LocalPosition;
            newPointVisual.transform.localScale = pointSetup.LocalScale;

            CombatTargetPointController targetPointController = newPointVisual.GetComponent<CombatTargetPointController>();

            if (targetPointController == null) targetPointController = newPointVisual.AddComponent<CombatTargetPointController>();

            targetPointController.Initialize(pointSetup.TargetPointData);
            _targetPointControllers.Add(targetPointController);
        }
    }
    
    public void SelectPoint(int direction)
    {
        _targetSelector?.SelectPoint(direction); // pases the responsibility to target selector -- i think this makes sure the enemy can access the target poitns
    }
    
    public void DamageSelectedTarget(int damageAmount)
    {
        if (SelectedTargetPoint == null) return;
        if (IsDead) return;

        string targetPointName = SelectedTargetPoint.PointData.TargetPointName;
        int actualDamageTaken = SelectedTargetPoint.TakeDamage(damageAmount);

        _bodyController.TryExposeCore();

        Debug.Log(gameObject.name + " took " + actualDamageTaken + " damage on " + targetPointName);
        Debug.Log(gameObject.name + " total HP: " + CurrentHealth + "/" + MaxHealth);

        if (SelectedTargetPoint.IsBroken)
        {
            Debug.Log(targetPointName + " is broken.");
        }

        if (IsDead)
        {
            Debug.Log(gameObject.name + " has been defeated.");
        }
    }
    
    
}