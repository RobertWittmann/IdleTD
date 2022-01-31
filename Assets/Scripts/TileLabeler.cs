using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.black;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.red;
    [SerializeField] Color pathColor = Color.green;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
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
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    private void DisplayCoordinates()
    {
        if (gridManager == null) return;

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.GridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.y / gridManager.GridSize);
        Node node = gridManager.GetNode(coordinates);
        if (node == null) return;

        label.text = $"{coordinates.x}, {coordinates.y} \n {node.weight}";
    }
}
