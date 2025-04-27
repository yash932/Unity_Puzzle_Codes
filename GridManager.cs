using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;

    [Header("Tray Settings")]
    [SerializeField] private DraggableObject _trayPrefab;  // Your SideTray prefab with DraggableObject attached
    [SerializeField] private int _trayCount = 5;           // Number of trays to spawn randomly

    private Dictionary<Vector2, Tile> _tiles;

    private void Start()
    {
        GenerateGrid();
        SpawnRandomTrays();
    }

    private void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                bool isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        if (_cam != null)
        {
            _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10f);
        }
    }

    private void SpawnRandomTrays()
    {
        if (_trayPrefab == null)
        {
            Debug.LogWarning("Tray Prefab not assigned in GridManager!");
            return;
        }

        List<Vector2> availablePositions = new List<Vector2>(_tiles.Keys);

        for (int i = 0; i < _trayCount; i++)
        {
            if (availablePositions.Count == 0) break;

            int randomIndex = Random.Range(0, availablePositions.Count);
            Vector2 randomPos = availablePositions[randomIndex];
            availablePositions.RemoveAt(randomIndex);

            DraggableObject tray = Instantiate(_trayPrefab, new Vector3(randomPos.x, randomPos.y, 0f), Quaternion.identity);
            tray.name = $"Tray {i}";
        }
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
            return tile;

        return null;
    }
}

