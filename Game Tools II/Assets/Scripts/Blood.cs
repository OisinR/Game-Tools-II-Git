using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour, Ipoolable {

    float cooldown = 3;


	public void OnObjectPooled ()
    {
        gameObject.SetActive(true);                     //The particle system plays on awake, so this turns it on
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
                gameObject.SetActive(false);        //deactivates it after playing so it can be used somewhere else
            }
        }
    }
}
