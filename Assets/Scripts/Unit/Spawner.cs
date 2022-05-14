using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private int _defaultPoolSize = 10;
    [SerializeField] private int _maxPoolSize = 25;

    private Vector2Int[] _spawnPositions;

    [SerializeField] float _spawnInterval = 0.5f;
    [SerializeField] bool _isSpawning = true;

    private NodeSystem _nodeSystem;
    private int _spawnPosIndex = 0;
    private ObjectPool<Unit> _pool;
    private AStar _pathFinder;
    private List<Node>[] _paths;

    private void Awake()
    {
        _nodeSystem = GetComponent<NodeSystem>();
    }

    private void Start()
    {
        _spawnPositions = _nodeSystem.SpawnPositions;
        _pool = new ObjectPool<Unit>(CreateUnit, GetUnit, ReleaseUnit, DestroyUnit, false, _defaultPoolSize, _maxPoolSize);
        _pathFinder = new AStar();

        SetPaths();
        StartCoroutine(Spawn());
    }

    private void SetPaths()
    {
        _paths = new List<Node>[_spawnPositions.Length];
        for (int i = 0; i < _spawnPositions.Length; i++)
        {
            Node _spawnNode = _nodeSystem.GetNode(_spawnPositions[i]);
            _paths[i] = _pathFinder.FindPath(_spawnNode, _nodeSystem.NodeGrid);
        }
    }

    IEnumerator Spawn()
    {
        while (_isSpawning)
        {
            if (_pool.CountActive < _maxPoolSize)
            {
                var unit = _pool.Get();
                unit.OnCreation();
            }
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private Unit CreateUnit()
    {
        var unit = Instantiate(_unit);

        unit.Init(this, KillUnit, _paths[_spawnPosIndex], _nodeSystem.GetNode(_spawnPositions[_spawnPosIndex]));

        _spawnPosIndex++;
        if (_spawnPosIndex >= _spawnPositions.Length) _spawnPosIndex = 0;

        return unit;
    }

    private void KillUnit(Unit unit)
    {
        _pool.Release(unit);
    }

    public void GetUnit(Unit obj)
    {
        obj.gameObject.SetActive(true);
    }

    public void ReleaseUnit(Unit obj)
    {
        obj.gameObject.SetActive(false);
    }

    public void DestroyUnit(Unit obj)
    {
        Destroy(obj.gameObject);
    }

    public List<Node> GetSpawnerPath(Vector2Int spawnPos)
    {
        for (int i = 0; i < _spawnPositions.Length; i++)
        {
            if (_spawnPositions[i] == spawnPos)
            {
                return _paths[i];
            }
        }
        Debug.Log("No spawn path found");
        return null;
    }
}
