using System.Collections.Generic;
using UnityEngine;

public class NodeSystem : MonoBehaviour
{
    [SerializeField] private int _gridSize;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private Vector2Int[] _spawnPositions;
    public Vector2Int[] SpawnPositions { get { return _spawnPositions; } }

    private Dictionary<Vector2Int, Node> _nodeGrid;
    public Dictionary<Vector2Int, Node> NodeGrid { get { return _nodeGrid; } }

    private int _minX;
    private int _minY;
    private int _maxX;
    private int _maxY;
    private AStar aStar;

    void Start()
    {
        _nodeGrid = new Dictionary<Vector2Int, Node>();
        aStar = new AStar();

        _minX = -_gridSize / 2;
        _minY = -_gridSize / 2;
        _maxX = (_gridSize / 2) + 1;
        _maxY = (_gridSize / 2) + 1;
        GenerateTiles();
        GenerateNodes();
    }

    public bool AvailablePath()
    {
        foreach (Vector2Int _spawnPosition in _spawnPositions)
        {
            if (aStar.FindPath(GetNode(_spawnPosition), _nodeGrid) == null)
            {
                return false;
            }
        }
        return true;
    }

    public Node GetNode(Vector2Int pos)
    {
        return _nodeGrid[pos];
    }

    private void GenerateTiles()
    {
        for (int x = _minX; x < _maxX; x++)
        {
            for (int y = _minY; y < _maxY; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                Instantiate(_tilePrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity, transform);
            }
        }
    }

    private void GenerateNodes()
    {
        for (int x = _minX; x < _maxX; x++)
        {
            for (int y = _minY; y < _maxY; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                Node newNode = new Node(pos);

                _nodeGrid[pos] = newNode;
            }
        }
    }

}
