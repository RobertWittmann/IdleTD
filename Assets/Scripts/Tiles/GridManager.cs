using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject spawnerNorth;
    [SerializeField] GameObject spawnerEast;
    [SerializeField] GameObject spawnerSouth;
    [SerializeField] GameObject spawnerWest;
    [SerializeField] int gridDimension;
    Vector2Int gridDimensions;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private void Awake()
    {
        GenerateTiles();
    }

    private void GenerateTiles()
    {
        gridDimensions.x = gridDimension;
        gridDimensions.y = gridDimension;

        for (int x = (-gridDimension / 2); x < (gridDimension / 2) + 1; x++)
        {
            for (int y = (-gridDimension / 2); y < (gridDimension / 2) + 1; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));

                if (x == 0 && y == -gridDimension / 2)
                {
                    // GameObject spawnTile = Instantiate(spawnerNorth, new Vector3(x, y, 0f), Quaternion.identity, transform);
                }
                else if (x == 0 && y == gridDimension / 2)
                {
                    // GameObject spawnTile = Instantiate(spawnerSouth, new Vector3(x, y, 0f), Quaternion.identity, transform);
                }
                else if (y == 0 && x == -gridDimension / 2)
                {
                    GameObject spawnTile = Instantiate(spawnerWest, new Vector3(x, y, 0f), Quaternion.identity, transform);
                }
                else if (y == 0 && x == gridDimension / 2)
                {
                    // GameObject spawnTile = Instantiate(spawnerEast, new Vector3(x, y, 0f), Quaternion.identity, transform);
                }
                else
                {
                    GameObject newTile = Instantiate(tilePrefab, new Vector3(x, y, 0f), Quaternion.identity, transform);
                }
            }
        }
    }

    public Node GetNode(Vector2Int coordinates)
    {
        return grid[coordinates];
    }
}
