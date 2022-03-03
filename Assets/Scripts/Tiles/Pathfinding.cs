using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] GameEvent pathFound;
    [SerializeField] GameEvent noPathFound;
    Vector2Int startCoordinates;
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
    List<Node> path = new List<Node>();
    List<Node> emptyPath = new List<Node>();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
        }
    }

    public List<Node> AStar(Vector2Int origin, Vector2Int destination)
    {
        startNode = grid[origin];
        startNode.minDistance = 0;
        destinationNode = grid[destination];
        ResetPath();
        openList.Add(startNode);
        while (openList.Count > 0)
        {

            currentSearchNode = openList.First();

            foreach (Node node in openList)
            {
                if (node.f < currentSearchNode.f) currentSearchNode = node;
            }

            openList.Remove(currentSearchNode);
            closedList.Add(currentSearchNode);

            if (currentSearchNode == destinationNode)
            {
                path = BuildPath();
                break;
            }

            List<Node> neighbours = FindNeighbours();
            foreach (Node child in neighbours)
            {
                if (closedList.Contains(child) || !child.isWalkable)
                {
                    continue;
                }
                if (openList.Contains(child))
                {
                    if (currentSearchNode.g + child.weight > child.g)
                    {
                        continue;
                    }
                }

                child.g = currentSearchNode.g + child.weight;
                child.h = DistanceToDestination(child);
                child.f = child.g + child.h;
                child.prevNode = currentSearchNode;

                openList.Add(child);
            }
        }
        if (openList.Count == 0)
        {
            noPathFound.Raise();
        }
        return path;
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
                Node neighbour = grid[neighbourCoords];
                neighbours.Add(neighbour);
            }
        }
        return neighbours;
    }

    private List<Node> BuildPath()
    {
        path.Clear();
        Node currentBuildNode = destinationNode;

        while (currentBuildNode != startNode)
        {
            path.Add(currentBuildNode);
            currentBuildNode.isPath = true;
            currentBuildNode = currentBuildNode.prevNode;
        }
        path.Add(currentBuildNode);
        currentBuildNode.isPath = true;
        path.Reverse();
        pathFound.Raise();
        return path;
    }

    private void ResetPath()
    {
        openList.Clear();
        closedList.Clear();
        foreach (KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }
}