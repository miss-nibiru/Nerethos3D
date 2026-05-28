using UnityEngine;

namespace _Project.Code.Gameplay.Level
{
    // Reads a LevelData asset (text maze + legend), builds the walkable grid, and spawns the tiles.
    public class StrongholdLevel : MonoBehaviour
    {
        [SerializeField] private StrongholdData _levelData;
        [SerializeField] private float _cellSize = 4f;
        
        private int _width;
        private int _height;
        
        //private readonly Dictionary<Vector2Int, GameObject> _collectables = new(); to implement in the future to get pickups in the grid that facey can interact with

        //public Vector2Int PlayerSpawn { get; private set; } = new Vector2Int(1, 1);

        private void Awake()
        {
            BuildLevel();
        }

        private void BuildLevel()
        {
            if (_levelData == null)
            {
                return;
            }

            // Drop the empty final row left by the layout's trailing newline.
            string[] rows = _levelData.Layout.Replace("\r", "").Split('\n');
            int rowCount = rows.Length;
            while (rowCount > 0 && rows[rowCount - 1].Length == 0)
            {
                rowCount--;
            }

            _height = rowCount;
            _width = 0;
            for (int y = 0; y < _height; y++)
            {
                if (rows[y].Length > _width)
                {
                    _width = rows[y].Length;
                }
            }
            //_walkable = new bool[_width, _height];

            StrongholdLegend legend = _levelData.Legend;

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    // Short rows are padded with floor (space) past their end.
                    char symbol = x < rows[y].Length ? rows[y][x] : ' ';

                    //_walkable[x, y] = legend.IsWalkable(symbol);

                    // if (symbol == LevelLegend.PlayerSpawn)
                    // {
                    //     PlayerSpawn = new Vector2Int(x, y);
                    // }

                    if (legend.TryGetPrefab(symbol, out GameObject prefab))
                    {
                        Vector2Int cell = new Vector2Int(x, y);
                        GameObject spawned = Instantiate(prefab, GridToWorld(cell), Quaternion.identity, transform);

                        // if (symbol == LevelLegend.Collectable)
                        // {
                        //     _collectables[cell] = spawned;
                        // }
                    }
                }
            }
        }

        // Remove the collectable on this cell, if any. Returns true if something was collected. future implementation
        // public bool TryCollect(Vector2Int cell)
        // {
        //     if (_collectables.TryGetValue(cell, out GameObject collectable))
        //     {
        //         _collectables.Remove(cell);
        //         Destroy(collectable);
        //         return true;
        //     }
        //
        //     return false;
        // }

        // Grid cell -> world. Row 0 sits at +Z; higher rows go toward -Z (so the world matches the text).
        public Vector3 GridToWorld(Vector2Int cell)
        {
            return new Vector3(cell.x * _cellSize, 0f, -cell.y * _cellSize);
        }

        // World -> grid cell. A spawned mover uses this to find the cell it was placed on.
        // public Vector2Int WorldToGrid(Vector3 worldPos)
        // {
        //     int x = Mathf.RoundToInt(worldPos.x / _cellSize);
        //     int y = Mathf.RoundToInt(-worldPos.z / _cellSize);
        //     return new Vector2Int(x, y);
        // }

        // public bool IsWalkable(int x, int y)
        // {
        //     bool insideGrid = x >= 0 && x < _width && y >= 0 && y < _height;
        //     if (!insideGrid)
        //     {
        //         return false;
        //     }
        //
        //     return _walkable[x, y];
        // }
    }
}