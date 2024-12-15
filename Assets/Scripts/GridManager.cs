using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab_normal;         // Prefab for normal tiles
    public GameObject tilePrefab_castle_red;     // Prefab for castle tiles
    public GameObject tilePrefab_treasure_yellow; // Prefab for treasure tiles
    public GameObject tilePrefab_objective_blue;  // Prefab for objective tiles
    public GameObject tilePrefab_portal_green;    // Prefab for portal tiles
    public int gridSize = 9;       // Board size (9x9 grid)
    public float tileSize = 1f;    // Tile size

    private GameObject[,] grid;    // Store the created grid

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new GameObject[gridSize, gridSize];

        // Offset to center the grid
        float offset = (gridSize - 1) * tileSize / 2;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 position = new Vector3(x * tileSize - offset, 0, y * tileSize - offset);
                GameObject tile;

                // Assign specific tile prefabs based on their type
                if (IsCastleTile(x, y))
                {
                    tile = Instantiate(tilePrefab_castle_red, position, Quaternion.identity);
                }
                else if (IsTreasureTile(x, y))
                {
                    tile = Instantiate(tilePrefab_treasure_yellow, position, Quaternion.identity);
                }
                else if (IsPortalTile(x, y))
                {
                    tile = Instantiate(tilePrefab_portal_green, position, Quaternion.identity);
                }
                else if (IsObjectiveTile(x, y))
                {
                    tile = Instantiate(tilePrefab_objective_blue, position, Quaternion.identity);
                }
                else
                {
                    tile = Instantiate(tilePrefab_normal, position, Quaternion.identity);
                }

                // Set the parent of the tile for better organization in the Hierarchy
                tile.transform.parent = transform;

                // Store the tile in the grid
                grid[x, y] = tile;
            }
        }
    }

    // Define castle tiles
    bool IsCastleTile(int x, int y) => (x == 0 && y == 4) || (x == 8 && y == 4);

    // Define treasure tiles
    bool IsTreasureTile(int x, int y) => (x == 8 && y == 0) || (x == 0 && y == 8);

    // Define portal tiles
    bool IsPortalTile(int x, int y) => (x == 4 && y == 0) || (x == 4 && y == 8);

    // Define objective tiles
    bool IsObjectiveTile(int x, int y)
    {
        return
            (x == 3 && y == 3) || (x == 3 && y == 4) || (x == 3 && y == 5) ||
            (x == 4 && y == 3) || (x == 4 && y == 4) || (x == 4 && y == 5) ||
            (x == 5 && y == 3) || (x == 5 && y == 4) || (x == 5 && y == 5);
    }
}