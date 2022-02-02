using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject spawnTilePrefab;
    [SerializeField] Vector2IntReference spawnPointNorth;
    [SerializeField] Vector2IntReference spawnPointEast;
    [SerializeField] Vector2IntReference spawnPointSouth;
    [SerializeField] Vector2IntReference spawnPointWest;
    [SerializeField] int gridDimension;
    [SerializeField] int tileScaleModifier = 200;
    Vector2Int gridDimensions;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }
    int gridSize;
    public int GridSize { get { return gridSize; } }

    private void Awake()
    {
        GenerateTiles();
    }

    private void GenerateTiles()
    {
        gridDimensions.x = gridDimension;
        gridDimensions.y = gridDimension;

        gridSize = tileScaleModifier / gridDimension;
        int tileScale = tileScaleModifier / gridDimension;
        tilePrefab.transform.localScale = new Vector3(tileScale, tileScale, tileScale);

        for (int x = (-gridDimension / 2); x < (gridDimension / 2) + 1; x++)
        {
            for (int y = (-gridDimension / 2); y < (gridDimension / 2) + 1; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true, Random.Range(1, 5) * 10));

                if (x == 0 && y == -gridDimension / 2)
                {
                    GameObject spawnTile = Instantiate(spawnTilePrefab, new Vector3(x * gridSize, y * gridSize, 0f), Quaternion.identity, transform);
                    spawnTile.GetComponent<Pathfinding>().startCoordinatesVariable = spawnPointNorth;
                }
                else if (x == 0 && y == gridDimension / 2)
                {
                    GameObject spawnTile = Instantiate(spawnTilePrefab, new Vector3(x * gridSize, y * gridSize, 0f), Quaternion.identity, transform);
                    spawnTile.GetComponent<Pathfinding>().startCoordinatesVariable = spawnPointSouth;
                }
                else if (y == 0 && x == -gridDimension / 2)
                {
                    GameObject spawnTile = Instantiate(spawnTilePrefab, new Vector3(x * gridSize, y * gridSize, 0f), Quaternion.identity, transform);
                    spawnTile.GetComponent<Pathfinding>().startCoordinatesVariable = spawnPointWest;
                }
                else if (y == 0 && x == gridDimension / 2)
                {
                    GameObject spawnTile = Instantiate(spawnTilePrefab, new Vector3(x * gridSize, y * gridSize, 0f), Quaternion.identity, transform);
                    spawnTile.GetComponent<Pathfinding>().startCoordinatesVariable = spawnPointEast;
                }
                else
                {
                    GameObject newTile = Instantiate(tilePrefab, new Vector3(x * gridSize, y * gridSize, 0f), Quaternion.identity, transform);
                }
            }
        }
    }

    public Node GetNode(Vector2Int coordinates)
    {
        return grid[coordinates];
    }
}
