using System;
using System.Collections.Generic;
using UnityEngine;

public static class PatternDiscovery
{
    private static readonly HashSet<PlayerAttackData> DiscoveredPatterns = new();

    public static event Action<PlayerAttackData> PatternDiscovered;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetForNewGame()
    {
        DiscoveredPatterns.Clear();
        PatternDiscovered = null;
    }

    public static bool IsDiscovered(PlayerAttackData attack)
    {
        return attack != null && (attack.StartsDiscovered || DiscoveredPatterns.Contains(attack));
    }

    public static void Discover(PlayerAttackData attack)
    {
        if (attack == null || IsDiscovered(attack)) return;
        DiscoveredPatterns.Add(attack);
        PatternDiscovered?.Invoke(attack);
    }
}