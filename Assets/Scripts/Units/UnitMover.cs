using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    public GameObject spawner;
    [SerializeField] float moveInterval;
    private Pathfinding pathfinding;
    private GridManager gridManager;
    private List<Node> unitPath;

    private void Awake()
    {
        pathfinding = GetComponent<Pathfinding>();
        gridManager = FindObjectOfType<GridManager>();
    }

    IEnumerator Move()
    {
        foreach (Node point in unitPath.ToList())
        {
            transform.position = new Vector3(point.coordinates.x, point.coordinates.y, 0);
            yield return new WaitForSeconds(moveInterval);
        }
        Destroy(gameObject);
    }

    public void SetPath(List<Node> path)
    {
        unitPath = path;
        StopCoroutine(Move());
        StartCoroutine(Move());
    }

    public void CorrectedPath()
    {
        StopCoroutine(Move());

        unitPath.Clear();
        unitPath = pathfinding.AStar(new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y)), new Vector2Int(0, 0));

        SetPath(unitPath);
    }
}
