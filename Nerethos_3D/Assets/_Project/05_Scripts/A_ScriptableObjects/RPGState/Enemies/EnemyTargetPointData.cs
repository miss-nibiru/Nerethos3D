using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  holds all the necessary information of each target point and how the weak point works on most regular enemies, non boss enemies
/// </summary>


public class EnemyTargetPointData : ScriptableObject

{
    
    [SerializeField] private string targetPointName;
    [SerializeField] private int targetMaxHealth;
    [SerializeField] private bool isWeakPoint; // if this point is a weak point, it can give better rewards when destroyed, but it does not have to be an instant kill
    [SerializeField] private float dropPercentage;
    
    
    public string TargetPointName => targetPointName;
    public int Health => targetMaxHealth;
    public bool IsWeakPoint => isWeakPoint;
    public float DropPercentage => dropPercentage;
    
    
    
    
    
}
