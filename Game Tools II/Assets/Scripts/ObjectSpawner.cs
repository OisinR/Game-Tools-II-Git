using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    [SerializeField] string[] tag;

    ObjectPooler objectPooler;

    [SerializeField] GameObject[] spawnpoints;

    float coolDown = 1;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void FixedUpdate()
    {
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
        else
        {
            coolDown = 1;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 10)
            {
                ToPool();
                
            }
        }
    }

    private void ToPool()
    {
        objectPooler.SpawnFromPool(tag[0], spawnpoints[Random.Range(0,spawnpoints.Length)].transform.position, transform.rotation);
    }
}
