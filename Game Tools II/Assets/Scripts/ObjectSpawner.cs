using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    [SerializeField] string[] tagg;                                                                         //changed to two g's to stop the warning message

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
            if (GameObject.FindGameObjectsWithTag("Enemy").Length/2 <= objectPooler.pools[0].size)                      //Enemy is tagged on two parts of each zombie, so this spawns the right amount
            {
                ToPool();
                
            }
        }
    }

    private void ToPool()
    {
        objectPooler.SpawnFromPool(tagg[0], spawnpoints[Random.Range(0,spawnpoints.Length)].transform.position, transform.rotation);             //spawns the enemies from the spawnpoints at random
    }
}
