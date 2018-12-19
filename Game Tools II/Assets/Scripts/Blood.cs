using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour, Ipoolable {

    float cooldown = 3;


	public void OnObjectPooled () {
        gameObject.SetActive(true);
	}


    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if(cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
            else
            {
                cooldown = 3;
                gameObject.SetActive(false);
            }
        }
    }
}
