using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MouseInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] GameEvent updateSpawnerPathfinding;
    [SerializeField] GameEvent updateUnitPathfinding;
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
                foreach (KeyValuePair<Vector2Int, Node> entry in grid)
                {
                    entry.Value.isExplored = false;
                    entry.Value.isPath = false;
                }

                Node node = gridManager.GetNode(hitInfo.transform.gameObject.GetComponent<TileColour>().Coordinates);
                node.isWalkable = !node.isWalkable;
                updateSpawnerPathfinding.Raise();
                updateUnitPathfinding.Raise();
                Debug.Log(node.coordinates);
            }
        }
        else
        {
            Debug.Log("Nothing hit");
        }
    }

    public void OnReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
