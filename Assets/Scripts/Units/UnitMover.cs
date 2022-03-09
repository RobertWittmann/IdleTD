using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    public Spawner spawner;
    public ObjectPool pool;
    [SerializeField] float moveInterval;
    private Pathfinding pathfinding;
    private GridManager gridManager;
    private List<Node> unitPath = new List<Node>();
    private Coroutine moving;

    private void Awake()
    {
        pathfinding = GetComponent<Pathfinding>();
        gridManager = FindObjectOfType<GridManager>();
    }

    IEnumerator Move()
    {
        for (int i = 0; i < unitPath.Count; i++)
        {
            transform.position = new Vector3(unitPath[i].coordinates.x, unitPath[i].coordinates.y, 0);
            if (i > 0)
            {
                unitPath[i - 1].hasEnemy = false;
            }
            unitPath[i].hasEnemy = true;
            yield return new WaitForSeconds(moveInterval);
        }
        pool.ReturnPoolObject(this.gameObject);
    }
    public void CorrectedPath()
    {
        if (moving != null) StopCoroutine(moving);

        List<Node> gridPath = spawner.GetPath();

        unitPath = pathfinding.AStar(new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y)), new Vector2Int(0, 0));

        SetPath(unitPath);
    }

    public void SetPath(List<Node> path)
    {
        unitPath = path;
        moving = StartCoroutine(Move());
    }
}