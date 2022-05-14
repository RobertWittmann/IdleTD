using UnityEngine;

public class Node
{
    public Vector2Int pos;
    public Node parent;

    public float g;
    public float h;
    public float f;
    public float w;

    public bool isWalkable;
    public bool isPath;
    public bool isDestination = false;
    public bool hasUnit = false;

    public Node(Vector2Int pos)
    {
        this.pos = pos;
        parent = null;

        w = 1;
        isWalkable = true;
    }
}