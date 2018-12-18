using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{


    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public static ObjectPooler Instance;

    public List<Pool> pools;

    Dictionary<string, Queue<GameObject>> poolsDictionaray;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(this);
            }
        }
    }
    private void Start()
    {
        poolsDictionaray = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectpool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject go = Instantiate(pool.prefab);
                go.SetActive(false);
                objectpool.Enqueue(go);
            }

            poolsDictionaray.Add(pool.tag, objectpool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolsDictionaray.ContainsKey(tag))
        {
            return null;
        }
        GameObject go = poolsDictionaray[tag].Dequeue();
        if (go.activeInHierarchy)
        {
            poolsDictionaray[tag].Enqueue(go);
            return go;
        }
        go.SetActive(true);
        go.transform.position = position;
        go.transform.rotation = rotation;

        Rigidbody pRb = go.AddComponent<Rigidbody>();
        pRb.constraints = RigidbodyConstraints.FreezeRotation;

        poolsDictionaray[tag].Enqueue(go);
        Ipoolable objectToPool = go.GetComponent<Ipoolable>();
        objectToPool.OnObjectPooled();

        return go;
    }

}
