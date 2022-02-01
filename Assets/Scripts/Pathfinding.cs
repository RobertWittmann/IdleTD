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

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    List<Node> openList = new List<Node>();
    List<Node> closedList = new List<Node>();

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
        AStar();
    }

    private void AStar()
    {
        openList.Add(startNode);
        StartCoroutine(SlowPathfinding());

    }

    IEnumerator SlowPathfinding()
    {
        while (openList.Count > 0)
        {
            openList = openList.OrderBy(n => n.f).ToList();

            currentSearchNode = openList.First();
            openList.Remove(currentSearchNode);
            currentSearchNode.isExplored = true;
            closedList.Add(currentSearchNode);

            if (currentSearchNode == destinationNode)
            {
                BuildPath();
                break;
            }

            List<Node> neighbours = FindNeighbours();
            foreach (Node child in neighbours)
            {
                if (closedList.Contains(child))
                {
                    continue;
                }
                child.g = currentSearchNode.g + child.weight;
                child.h = DistanceToDestination(child);
                child.f = child.g + child.h;

                if (openList.Contains(child))
                {
                    if (child.g > openList[openList.IndexOf(child)].g)
                    {
                        continue;
                    }
                }
                child.prevNode = currentSearchNode;
                openList.Add(child);
            }
            yield return new WaitForSeconds(0.0f);
        }
    }

    private int DistanceToDestination(Node node)
    {
        int distanceX = Mathf.Abs(destinationNode.coordinates.x - node.coordinates.x);
        int distanceY = Mathf.Abs(destinationNode.coordinates.y - node.coordinates.y);
        return (int)(Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceY, 2));
    }

    private List<Node> FindNeighbours()
    {
        List<Node> neighbours = new List<Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoords = currentSearchNode.coordinates + direction;
            if (grid.ContainsKey(neighbourCoords))
            {
                neighbours.Add(grid[neighbourCoords]);
            }
        }
        return neighbours;
    }

    private void BuildPath()
    {
        Node currentBuildNode = destinationNode;

        while (currentBuildNode != startNode)
        {
            currentBuildNode.isPath = true;
            currentBuildNode = currentBuildNode.prevNode;
        }
    }
}