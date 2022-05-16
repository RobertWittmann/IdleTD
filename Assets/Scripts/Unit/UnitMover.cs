using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    private Unit _unit;

    private AStar _pathFinder;
    private NodeSystem _nodeSystem;
    private List<Vector2Int> _path;

    private Vector2Int _pos;
    private Coroutine _moveCoroutine = null;


    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _pathFinder = new AStar();
        _nodeSystem = FindObjectOfType<NodeSystem>();
    }

    public void InitiateMover(Node origin, Dictionary<Vector2Int, Node> nodeDict)
    {
        _path = _pathFinder.FindPath(origin, nodeDict);
        ResetMover();
    }
    public void InitiateMover(List<Vector2Int> path)
    {
        _path = path;
        ResetMover();
    }

    private void ResetMover()
    {
        if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
        _moveCoroutine = StartCoroutine(MoveUnit());
    }

    IEnumerator MoveUnit()
    {
        for (int i = 0; i < _path.Count; i++)
        {
            if (i > 0)
            {
                Node _prevNode = _nodeSystem.GetNode(_path[i - 1]);
                _prevNode.hasUnit = false;
            }
            Node _node = _nodeSystem.GetNode(_path[i]);
            _node.hasUnit = true;
            transform.position = (Vector2)_path[i];
            yield return new WaitForSeconds(_unit._Stats._moveInterval);
        }

        _unit.OnDestination();
    }
}
