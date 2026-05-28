using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Gameplay.Level
{
    /// <summary>
    /// Maps level-layout characters to the prefabs that represent them, and records which
    /// characters are walkable. Symbols may mean terrain (wall, floor) or an actor/item spawned
    /// on the cell (player, ghost, collectable). Authored as a reusable asset so levels share one legend.
    /// </summary>
    
    [CreateAssetMenu(fileName = "StrongholdLegend", menuName = "Game/Stronghold Legend")]
    public class StrongholdLegend : ScriptableObject
    {
        public const char Wall = 'W';
        public const char Collectable = 'C';
        public const char OutsideFloor = 'O';
        public const char WindowWall = 'X';
        public const char DoorWall = 'Y';
        public const char InsideFloor = ' ';

        [SerializeField] private List<StrongholdLegendEntry> _entries = new();

        public bool TryGetPrefab(char symbol, out GameObject prefab)
        {
            foreach (StrongholdLegendEntry entry in _entries)
            {
                if (entry.Symbol == symbol)
                {
                    prefab = entry.Prefab;
                    return prefab != null;
                }
            }

            prefab = null;
            return false;
        }

        public bool IsWalkable(char symbol)
        {
            foreach (StrongholdLegendEntry entry in _entries)
            {
                if (entry.Symbol == symbol)
                {
                    return entry.Walkable;
                }
            }

            // Unmapped characters are treated as floor, which is walkable.
            return true;
        }
    }
    
    [System.Serializable]
    public class StrongholdLegendEntry
    {
        [SerializeField] private string _symbol = "#";
        [SerializeField] private GameObject _prefab;
        [SerializeField] private bool _walkable;

        public GameObject Prefab => _prefab;
        public bool Walkable => _walkable;

        /// <summary>The authored symbol as a character. Defaults to a space if the string is empty.</summary>
        public char Symbol => string.IsNullOrEmpty(_symbol) ? ' ' : _symbol[0];
    }
    
}