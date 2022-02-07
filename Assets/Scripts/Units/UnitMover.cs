using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    public GameObject spawner;
    [SerializeField] float moveInterval;
    [SerializeField] bool moving = true;
    private Pathfinding pathfinding;
    private GridManager gridManager;
    private List<Node> path;

    private void Awake()
    {
        pathfinding = GetComponent<Pathfinding>();
        gridManager = FindObjectOfType<GridManager>();
    }

    IEnumerator Move()
    {
        foreach (Node point in path)
        {
            transform.position = new Vector3(point.coordinates.x, point.coordinates.y, 0);
            yield return new WaitForSeconds(moveInterval);
        }
        Destroy(gameObject);
    }

    public void SetPath(List<Node> path)
    {
        this.path = path;
        StopAllCoroutines();
        StartCoroutine(Move());
    }

    public void UpdatePath()
    {
        StopAllCoroutines();
        Vector2Int cloestPoint = new Vector2Int();
        float dist = Mathf.Infinity;
        foreach (Node point in path)
        {
            float newDist = Vector2Int.Distance(new Vector2Int((int)transform.position.x, (int)transform.position.y), point.coordinates);
            if (newDist < dist)
            {
                dist = newDist;
                cloestPoint = point.coordinates;
            }
        }
        Debug.Log("dist: " + dist);
        if (dist == 0)
        {
            ContinuePath();
        }
        else
        {
            path = pathfinding.AStar(cloestPoint);
            StartCoroutine(ConnectToPath());
        }
    }

    IEnumerator ConnectToPath()
    {
        foreach (Node point in path)
        {
            transform.position = new Vector3(point.coordinates.x, point.coordinates.y, 0);
            yield return new WaitForSeconds(moveInterval);
        }
        ContinuePath();
    }

    private void ContinuePath()
    {
        List<Node> spawnerPath = spawner.GetComponent<Spawner>().GetPath();

        for (int i = 0; i < spawnerPath.Count; i++)
        {
            if (spawnerPath[i].coordinates != new Vector2Int((int)transform.position.x, (int)transform.position.y))
            {
                spawnerPath.Remove(spawnerPath[i]);
            }
            else
            {
                break;
            }
        }
        path = spawnerPath;
        StopAllCoroutines();
        StartCoroutine(Move());
    }
}
