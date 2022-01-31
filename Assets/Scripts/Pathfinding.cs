using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; } }

    [SerializeField] Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates { get { return destinationCoordinates; } }

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            startNode.minDistance = 0;
            destinationNode = grid[destinationCoordinates];
        }
    }

    private void Start()
    {
        DijkstraSearch();
    }

    private void DijkstraSearch()
    {
        List<Node> queue = new List<Node>();
        queue.Add(startNode);
        while (queue.Any())
        {
            queue = queue.OrderBy(x => x.minDistance).ToList();
            Node node = queue.First();
            queue.Remove(node);
            List<Node> neighbours = new List<Node>();
            foreach (Vector2Int direction in directions)
            {
                Vector2Int neighbourCoords = node.coordinates + direction;
                if (grid.ContainsKey(neighbourCoords))
                {
                    neighbours.Add(grid[neighbourCoords]);
                }
            }
            foreach (Node neighbour in neighbours)
            {

            }
        }
    }
}