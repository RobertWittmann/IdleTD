using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] GameEvent updatePathfinding;
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
                Node node = gridManager.GetNode(hitInfo.transform.gameObject.GetComponent<TileColour>().Coordinates);
                node.isWalkable = !node.isWalkable;
                updatePathfinding.Raise();
                Debug.Log(node.coordinates);
            }
        }
        else
        {
            Debug.Log("Nothing hit");
        }
    }
}
