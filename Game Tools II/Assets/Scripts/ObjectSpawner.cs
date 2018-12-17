using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    [SerializeField] string[] tag;

    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void ToPool()
    {
        objectPooler.SpawnFromPool(tag[0], transform.position, transform.rotation);
    }
}
