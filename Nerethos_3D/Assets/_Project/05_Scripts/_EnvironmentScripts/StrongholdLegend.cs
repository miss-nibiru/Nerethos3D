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

        [Header("Floors")]
        [SerializeField] private List<StrongholdLegendEntry> _floorEntries = new();

        [Header("Walls")]
        [SerializeField] private List<StrongholdLegendEntry> _wallEntries = new();

        [Header("Miscellaneous")]
        [SerializeField] private List<StrongholdLegendEntry> _miscEntries = new();

        [Header("Pipes")]
        [SerializeField] private List<StrongholdLegendEntry> _pipeEntries = new();

        public bool TryGetPrefabFromList(char symbol, out GameObject prefab, out Vector3 rotation)
        {
            if (TryGetPrefabFromList(_floorEntries, symbol, out prefab, out rotation))
            {
                return true;
            }
            
            if (TryGetPrefabFromList(_wallEntries, symbol, out prefab, out rotation))
            {
                return true;
            }

            if (TryGetPrefabFromList(_miscEntries, symbol, out prefab, out rotation))
            {
                return true;
            }

            if (TryGetPrefabFromList(_pipeEntries, symbol, out prefab, out rotation))
            {
                return true;
            }

            prefab = null;
            rotation = Vector3.zero;
            return false;
        }
        
        private bool TryGetPrefabFromList(List<StrongholdLegendEntry> entries, char symbol, out GameObject prefab, out Vector3 rotation)
        {
            foreach (StrongholdLegendEntry entry in entries)
            {
                if (entry.Symbol == symbol)
                {
                    prefab = entry.Prefab;
                    rotation = entry.Rotation;
                    return prefab != null;
                }
            }

            prefab = null;
            rotation = Vector3.zero;
            return false;
        }

        public bool IsWalkable(char symbol)
        {
            if (TryGetWalkableFromList(_floorEntries, symbol, out bool floorWalkable))
            {
                return floorWalkable;
            }

            if (TryGetWalkableFromList(_wallEntries, symbol, out bool wallWalkable))
            {
                return wallWalkable;
            }

            return true;
        }
        
        
        private bool TryGetWalkableFromList(List<StrongholdLegendEntry> entries, char symbol, out bool walkable)
        {
            foreach (StrongholdLegendEntry entry in entries)
            {
                if (entry.Symbol == symbol)
                {
                    walkable = entry.Walkable;
                    return true;
                }
            }

            walkable = true;
            return false;
        }
        
        
    }
    
    [System.Serializable]
    public class StrongholdLegendEntry
    {
        [SerializeField] private string _symbol = "#";
        [SerializeField] private GameObject _prefab;
        [SerializeField] private bool _walkable;
        
        [SerializeField] private Vector3 _rotation;

        public GameObject Prefab => _prefab;
        public bool Walkable => _walkable;
        
        public Vector3 Rotation => _rotation;

        /// <summary>The authored symbol as a character. Defaults to a space if the string is empty.</summary>
        public char Symbol => string.IsNullOrEmpty(_symbol) ? ' ' : _symbol[0];
    }
    
}