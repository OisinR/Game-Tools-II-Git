using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    //Public Variables
    public Slider healthBar;
    public string opponent;
    public float hitTimer;
    public float hitCoolDown;

    //Private Variables
    Animator anim;
    private bool pDead;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void FixedUpdate()
    {
        if (healthBar.value <= 0 && !pDead)                         //if health reachs 0, die. bool to stop the trigger being constantly called
        {
            anim.SetTrigger("Death");
            pDead = true;
            GetComponent<camMouseLook>().enabled = false;           //stops the mouse from turning the player
        }

        if (hitTimer > 0)
        {
            hitTimer -= Time.deltaTime;                             //cooldwon on damage so the healthbar doesnt drain in one frame
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != opponent)                       //if the tag on the other is Enemy, damage the player
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
