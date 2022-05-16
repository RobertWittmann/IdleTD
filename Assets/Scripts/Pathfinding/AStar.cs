using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStar
{
    private Node startNode;
    private Node endNode;

    public List<Vector2Int> FindPath(Node origin, Dictionary<Vector2Int, Node> nodeDict)
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
                List<Vector2Int> _path = BuildPath(currentNode);
                ResetNodes(nodeDict);
                return _path;
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

    private List<Vector2Int> BuildPath(Node node)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        while (node.parent != null)
        {
            node.isPath = true;
            path.Add(node.pos);
            node = node.parent;
        }
        node.isPath = true;
        path.Add(node.pos);
        path.Reverse();

        return path;
    }

    private void ResetNodes(Dictionary<Vector2Int, Node> dict)
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in dict)
        {
            entry.Value.g = 0;
            entry.Value.h = 0;
            entry.Value.isPath = false;
            entry.Value.parent = null;
        }
    }
}
