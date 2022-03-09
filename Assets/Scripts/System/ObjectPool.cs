using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public IObjectPool<GameObject> objectPool;
    [SerializeField] private GameObject poolObject;
    [SerializeField] private IntReference poolSizeMax;

    private void Start()
    {
        objectPool = new ObjectPool<GameObject>(CreatePooledObject, actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 5, maxSize: poolSizeMax);
    }

    private GameObject CreatePooledObject()
    {
        GameObject unit = Instantiate(poolObject, transform.position, Quaternion.identity, transform);
        return unit;
    }

    public GameObject GetPoolObject()
    {
        Debug.Log(objectPool.CountInactive);
        return objectPool.Get();
    }

    public void ReturnPoolObject(GameObject returnObject)
    {
        objectPool.Release(returnObject);
    }
}
