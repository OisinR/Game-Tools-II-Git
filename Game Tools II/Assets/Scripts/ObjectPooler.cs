using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{


    [System.Serializable]
    public class Pool
    {
        public string nameTag;
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

            poolsDictionaray.Add(pool.nameTag, objectpool);
        }
    }

    public GameObject SpawnFromPool(string nameTag, Vector3 position, Quaternion rotation)
    {
        if (!poolsDictionaray.ContainsKey(nameTag))
        {
            return null;
        }
        GameObject go = poolsDictionaray[nameTag].Dequeue();
        if (go.activeInHierarchy)
        {
            poolsDictionaray[nameTag].Enqueue(go);
            return go;
        }
        go.SetActive(true);
        go.transform.position = position;
        go.transform.rotation = rotation;
        if (go.tag != "Blood")
        {

            Rigidbody pRb = go.AddComponent<Rigidbody>();

            go.GetComponent<detectHit>().pDead = false;
            pRb.constraints = RigidbodyConstraints.FreezeRotation;

        }

        
        poolsDictionaray[nameTag].Enqueue(go);
        Ipoolable objectToPool = go.GetComponent<Ipoolable>();
        objectToPool.OnObjectPooled();

        return go;
    }

}
