using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] Vector2Int gridDimensions;
    [SerializeField] int gridSize = 10;
    public int GridSize { get { return gridSize; } }

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private void Awake()
    {
        GenerateTiles();
    }

    private void GenerateTiles()
    {
        for (int x = 0; x < gridDimensions.x; x++)
        {
            for (int y = 0; y < gridDimensions.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true, Random.Range(1, 5)));
                GameObject newTile = Instantiate(tilePrefab, new Vector3(x * gridSize, y * gridSize, 0f), Quaternion.identity);
            }
        }
    }

    public Node GetNode(Vector2Int coordinates)
    {
        return grid[coordinates];
    }
}
