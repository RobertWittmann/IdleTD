using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MouseInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] GameEvent updateSpawnerPathfinding;
    [SerializeField] GameEvent updateUnitPathfinding;
    [SerializeField] GameEvent towerClick;
    GridManager gridManager;

    private void Awake()
    {
        gridManager = GetComponent<GridManager>();
    }
    public void OnClick()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        RaycastHit2D hitInfo = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero);

        if (hitInfo.collider)
        {
            if (hitInfo.transform.gameObject.GetComponent<TileColour>())
            {
                Dictionary<Vector2Int, Node> grid = gridManager.Grid;

                Node node = gridManager.GetNode(hitInfo.transform.gameObject.GetComponent<TileColour>().Coordinates);

                if (node.coordinates == new Vector2Int(0, 0))
                {
                    DamangeAllUnits();
                }
                else if (!node.hasEnemy)
                {
                    node.isWalkable = !node.isWalkable;
                    foreach (KeyValuePair<Vector2Int, Node> entry in grid)
                    {
                        entry.Value.isExplored = false;
                        entry.Value.isPath = false;
                    }
                    updateSpawnerPathfinding.Raise();
                    updateUnitPathfinding.Raise();
                }
                // Debug.Log(node.coordinates);
            }
        }
        else
        {
            // Debug.Log("Nothing hit");
        }
    }

    private void DamangeAllUnits()
    {
        towerClick.Raise();
    }

    public void OnReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
