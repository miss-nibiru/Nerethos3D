using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this is the same enemy data idea but specifically for Facey. During combat she can get a bit more strained that the enemies.
/// Each target that gets broken should end up in something different/bad for player
/// </summary>

[CreateAssetMenu(fileName = "PlayerCombatantData", menuName = "Scriptable Objects/Player Combatant Data")]
public class PlayerCombatantData : ScriptableObject
{
    [System.Serializable]
    public class PlayerTargetPointSetup
    {
        [SerializeField] private CombatTargetPointData targetPointData;
        [SerializeField] private GameObject targetPointVisual;
        [SerializeField] private Vector3 localPosition;
        [SerializeField] private Vector3 localScale = Vector3.one;

        public CombatTargetPointData TargetPointData => targetPointData;
        public GameObject TargetPointVisual => targetPointVisual;
        public Vector3 LocalPosition => localPosition;
        public Vector3 LocalScale => localScale;
    }

    [Header("Player Main Stats")]
    [SerializeField] private string playerName;
    [SerializeField] private int attackPower;
    [SerializeField] private int defensePower;
    [SerializeField] private int speed;
    [SerializeField] private int madness;

    [Header("Target Points")]
    [SerializeField] private List<PlayerTargetPointSetup> targetPoints = new();

    public string PlayerName => playerName;
    public int AttackPower => attackPower;
    public int DefensePower => defensePower;
    public int Speed => speed;
    public int Madness => madness;
    public List<PlayerTargetPointSetup> TargetPoints => targetPoints;
}
