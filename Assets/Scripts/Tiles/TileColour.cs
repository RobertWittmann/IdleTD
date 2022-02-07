using UnityEngine;

public class TileColour : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.red;
    [SerializeField] Color pathColor = Color.green;

    Vector2Int coordinates = new Vector2Int();
    public Vector2Int Coordinates { get { return coordinates; } }
    GridManager gridManager;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (gridManager == null) return;
        coordinates.x = Mathf.RoundToInt(transform.position.x);
        coordinates.y = Mathf.RoundToInt(transform.position.y);
    }

    void Update()
    {
        SetLabelColour();
    }


    private void SetLabelColour()
    {
        if (gridManager == null) return;
        Node node = gridManager.GetNode(coordinates);

        if (node == null) return;

        if (!node.isWalkable)
        {
            spriteRenderer.color = blockedColor;
        }
        else if (node.isPath)
        {
            spriteRenderer.color = pathColor;
        }
        else if (node.isExplored)
        {
            spriteRenderer.color = exploredColor;
        }
        else
        {
            spriteRenderer.color = defaultColor;
        }
    }
}
