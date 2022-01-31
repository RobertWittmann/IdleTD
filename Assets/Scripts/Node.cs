using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int coordinates;
    public int weight;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Node prevNode;
    public float minDistance;

    public int g;
    public int h;
    public int f;

    public Node(Vector2Int coordinates, bool isWalkable, int weight)
    {
        this.coordinates = coordinates;
        this.weight = weight;
        this.isWalkable = isWalkable;

        minDistance = Mathf.Infinity;
        prevNode = null;

        g = 0;
        h = 0;
        f = 0;
    }
}
