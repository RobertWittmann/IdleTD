using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    private Unit _unit;

    private AStar _pathFinder;
    private List<Node> _path;

    private Vector2Int _pos;
    private Coroutine _moveCoroutine = null;


    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _pathFinder = new AStar();
    }

    public void InitiateMover(Node origin, Dictionary<Vector2Int, Node> nodeDict)
    {
        _path = _pathFinder.FindPath(origin, nodeDict);
        ResetMover();
    }
    public void InitiateMover(List<Node> path)
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
            _path[Mathf.Max(0, i - 1)].hasUnit = false;
            _path[i].hasUnit = true;
            transform.position = (Vector2)_path[i].pos;
            yield return new WaitForSeconds(_unit._Stats._moveInterval);
        }

        _unit.OnDestination();
    }
}
