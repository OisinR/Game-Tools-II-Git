using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Navmeshfix : MonoBehaviour {

    void Start()
    {
        FindNavMesh();
    }

    public void FindNavMesh()                                                                                                       //trys to find navmesh if unity cant
    {
        NavMeshHit closestHit;

        if (NavMesh.SamplePosition(gameObject.transform.position, out closestHit, 500f, NavMesh.AllAreas))
            gameObject.transform.position = closestHit.position;
        else
            Debug.LogError("Cant find NavMesh!");
    }
}
