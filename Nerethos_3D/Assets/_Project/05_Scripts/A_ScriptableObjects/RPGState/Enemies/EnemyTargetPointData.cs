using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  holds all the necessary information of each target point and how the weak point works on most regular enemies, nonboss enemies
/// </summary>

[CreateAssetMenu(fileName = "EnemyTargetPointData", menuName = "Scriptable Objects/EnemyTargetPointData")] // imporant to add on all scriptable objects to be detected as such!

public class EnemyTargetPointData : ScriptableObject

{
     
    
    [SerializeField] private string targetPointName;
    [SerializeField] private int targetMaxHealth;
    [SerializeField] private float dropPercentage;
    
    
    [SerializeField] private bool isWeakPoint; // if this point is a weak point, it can give better rewards when destroyed, but it does not have to be an instant kill
    [SerializeField] private bool isMainPoint;
    
    
    public string TargetPointName => targetPointName;
    public int Health => targetMaxHealth;
    public bool IsWeakPoint => isWeakPoint;
    public float DropPercentage => dropPercentage;
    
    public bool IsMainPoint => isMainPoint;
    
    
    
    
    
}
