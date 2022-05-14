using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStar
{
    private Node startNode;
    private Node endNode;

    public List<Node> FindPath(Node origin, Dictionary<Vector2Int, Node> nodeDict)
    {
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        startNode = origin;
        endNode = nodeDict[Vector2Int.zero];

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            openList = openList.OrderBy(o => o.f).ToList();
            Node currentNode = openList.First();
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == endNode)
            {
                return BuildPath(currentNode);
            }

            List<Node> neighbours = FindNeighbours(currentNode, nodeDict);

            foreach (Node neighbour in neighbours)
            {
                if (closedList.Contains(neighbour) || !neighbour.isWalkable) continue;


                neighbour.g = currentNode.g + neighbour.w;
                neighbour.h = ManhattanDistance(neighbour, endNode);
                neighbour.f = neighbour.g + neighbour.h;

                neighbour.parent = currentNode;

                if (openList.Contains(neighbour))
                {
                    foreach (Node node in openList)
                    {
                        if (neighbour.g > node.g) continue;
                    }
                }

                openList.Add(neighbour);
            }
        }

        Debug.Log("no path found");
        return null;
    }

    private List<Node> FindNeighbours(Node node, Dictionary<Vector2Int, Node> nodeDict)
    {
        List<Node> neighbours = new List<Node>();
        Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

        foreach (Vector2Int dir in directions)
        {
            Vector2Int newPos = node.pos + dir;
            if (nodeDict.ContainsKey(newPos))
            {
                Node neighbour = nodeDict[newPos];
                neighbours.Add(neighbour);
            }
        }
        return neighbours;
    }

    private float ManhattanDistance(Node startNode, Node endNode)
    {
        return (Mathf.Abs(startNode.pos.x - endNode.pos.x) + Mathf.Abs(startNode.pos.y - endNode.pos.y));
    }

    private List<Node> BuildPath(Node node)
    {
        List<Node> path = new List<Node>();
        while (node.parent != null)
        {
            node.isPath = true;
            path.Add(node);
            Node nodeParent = node.parent;
            node.parent = null;
            node.g = 0;
            node.h = 0;
            node = nodeParent;
        }
        node.isPath = true;
        path.Add(node);
        path.Reverse();

        return path;
    }
}
