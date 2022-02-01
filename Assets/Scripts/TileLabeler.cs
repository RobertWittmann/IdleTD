using UnityEngine;
using TMPro;

public class TileLabeler : MonoBehaviour
{


    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
    }

    private void Update()
    {
        DisplayCoordinates();
    }

    private void DisplayCoordinates()
    {
        if (gridManager == null) return;

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.GridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.y / gridManager.GridSize);
        Node node = gridManager.GetNode(coordinates);
        if (node == null) return;

        label.text = $"{coordinates.x}, {coordinates.y} \n w: {node.weight} \n f: {node.f}";
    }
}
