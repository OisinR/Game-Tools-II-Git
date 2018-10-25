using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{

    public Slider healthBar;
    Animator anim;
    public string opponent;
    private bool pDead;
    public float hitTimer;
    public float hitCoolDown;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void FixedUpdate()
    {
        if (healthBar.value <= 0 && !pDead)
        {
            anim.SetTrigger("Death");
            pDead = true;
        }

        if (hitTimer > 0)
        {
            hitTimer -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != opponent)
        {
            return;
        }
        else
        {
            if (hitTimer <= 0)
            {
                healthBar.value -= 10;
                hitTimer = hitCoolDown;
            }


        
        }

    }



}
