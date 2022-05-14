using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    [SerializeField] private GameEvent _onBuildingPlaced;

    private NodeSystem _nodeSystem;
    private Controls _controls;
    private Vector2 _mousePosition;


    private void Awake()
    {
        _nodeSystem = GetComponent<NodeSystem>();
    }

    private void Start()
    {
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();
    }

    private void OnDestroy()
    {
        _controls.Player.Disable();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Dictionary<Vector2Int, Node> grid = _nodeSystem.NodeGrid;
            Node node = _nodeSystem.GetNode(MouseToGridPosition());

            if (node == null) return;
            // if (node.hasUnit == true) return;

            if (node.isPath) node.isPath = false;
            node.isWalkable = !node.isWalkable;
            if (!_nodeSystem.AvailablePath())
            {
                node.isWalkable = !node.isWalkable;
                return;
            }
            _onBuildingPlaced.Raise();
        }
    }

    private Vector2Int MouseToGridPosition()
    {
        Vector2Int gridPosition = new Vector2Int();
        gridPosition.x = Mathf.RoundToInt(_mousePosition.x);
        gridPosition.y = Mathf.RoundToInt(_mousePosition.y);
        return gridPosition;
    }

    public void OnMouseMovement(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
        _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
    }
}
