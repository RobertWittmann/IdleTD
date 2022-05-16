using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitSO _stats;
    public UnitSO _Stats { get { return _stats; } }

    private Spawner _spawner;
    private UnitMover _unitMover;
    private NodeSystem _nodeSystem;
    private Node _spawnNode;
    private List<Vector2Int> _path;

    private Action<Unit> _killAction;

    private void Awake()
    {
        _unitMover = GetComponent<UnitMover>();
        _nodeSystem = FindObjectOfType<NodeSystem>();
    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _stats._sprite;
    }

    public void Init(Spawner spawner, Action<Unit> killAction, List<Vector2Int> path, Node spawnNode)
    {
        _spawner = spawner;
        _killAction = killAction;
        _path = path;
        _spawnNode = spawnNode;
    }

    public void OnBuildingPlaced()
    {
        Vector2Int _currentPos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        Node _currentNode = _nodeSystem.GetNode(_currentPos);
        if (!_currentNode.isWalkable) return;
        _unitMover.InitiateMover(_currentNode, _nodeSystem.NodeGrid);
    }

    public void OnDestination()
    {
        _path = _spawner.GetSpawnerPath(_spawnNode.pos);
        _unitMover.InitiateMover(_path);
    }

    public void OnCreation()
    {
        _unitMover.InitiateMover(_path);
    }
}
