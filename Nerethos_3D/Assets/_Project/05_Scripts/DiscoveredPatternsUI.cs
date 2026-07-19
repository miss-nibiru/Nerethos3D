using System.Collections.Generic;
using UnityEngine;

public class DiscoveredPatternsUI : MonoBehaviour
{
    [SerializeField] private Transform rowsParent;
    [SerializeField] private DiscoveredPatternRow rowPrefab;
    [SerializeField] private List<PlayerAttackData> displayedPatterns = new();

    private void Awake()
    {
        BuildRows();
    }

    private void BuildRows()
    {
        foreach (PlayerAttackData attack in displayedPatterns)
        {
            if (attack == null) continue;
            DiscoveredPatternRow row = Instantiate(rowPrefab, rowsParent);
            row.Setup(attack, GetContinuationDepth(attack));
        }
    }

    private int GetContinuationDepth(PlayerAttackData attack)
    {
        int depth = 0;
        PlayerAttackData currentAttack = attack;

        while (currentAttack.RequiresPreviousAttack && currentAttack.RequiredPreviousAttack != null && depth < 10)
        {
            depth++; currentAttack = currentAttack.RequiredPreviousAttack;
        }

        return depth;
    }
}