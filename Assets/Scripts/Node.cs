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

    public Node(Vector2Int coordinates, bool isWalkable, int weight)
    {
        this.coordinates = coordinates;
        this.weight = weight;
        this.isWalkable = isWalkable;

        minDistance = Mathf.Infinity;
        prevNode = null;
    }
}
