using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerRegen : MonoBehaviour
{

    public float healthbar = 100;
    public float healthRegen;
	void Start ()
    {
        InvokeRepeating("PlayerRegen", 2.0f, 4.0f);
	}

	void PlayerRegen ()
    {
        if (healthbar > 0)
        {
            healthbar += healthRegen;
            if (healthbar > 100)
            {
                healthbar = 100;
            }
        }
	}
}
