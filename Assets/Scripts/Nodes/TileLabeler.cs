using UnityEngine;
using TMPro;

public class TileLabeler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Color colorDefault;
    [SerializeField] private Color colorPath;
    [SerializeField] private Color colorBlocked;

    private NodeSystem nodeSystem;
    private Node tileNode;
    private Vector2Int pos;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        nodeSystem = FindObjectOfType<NodeSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        pos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        tileNode = nodeSystem.GetNode(pos);
    }

    void Update()
    {
        if (tileNode.isPath) spriteRenderer.color = colorPath;
        else if (tileNode.isWalkable) spriteRenderer.color = colorDefault;
        else spriteRenderer.color = colorBlocked;
        // label.text = $"({tileNode.pos.x},{tileNode.pos.y}) \n g:{tileNode.g} \n h:{tileNode.h} \n f:{tileNode.f} \n w:{tileNode.w}";
        // label.text = $"({tileNode.pos.x},{tileNode.pos.y}) \n has unit: {tileNode.hasUnit}";
        label.text = $"path: {tileNode.isPath} \n walkable: {tileNode.isWalkable}";
    }
}
