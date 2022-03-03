using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] FloatReference spawnInterval;
    [SerializeField] Vector2IntReference spawnPosition;
    [SerializeField] GameObject spawnUnit;
    [SerializeField] bool spawnActive = true;
    Pathfinding pathfinding;
    List<Node> path = new List<Node>();

    private void Awake()
    {
        pathfinding = GetComponent<Pathfinding>();
    }
    void Start()
    {
        NewPath();
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (spawnActive)
        {
            GameObject unit = Instantiate(spawnUnit, transform.position, Quaternion.identity, transform);
            unit.GetComponent<UnitMover>().spawner = transform.gameObject;
            unit.GetComponent<UnitMover>().SetPath(path);
            yield return new WaitForSeconds(spawnInterval.Value);
        }
    }

    public void NewPath()
    {
        path = pathfinding.AStar(new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y)), new Vector2Int(0, 0));
    }

    public List<Node> GetPath()
    {
        return path;
    }

}