using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{

    public bool attacking = false;
    private float attackTimer = 0;
    public float attackCoolDown = 0.3f;
    public Collider attackTrigger;


    void Awake()
    {
        attackTrigger.enabled = false;
    }

    void Update()
    {
        if (attackTrigger != null)
        {
            if (!attacking)
            {
                attacking = true;
                attackTimer = attackCoolDown;

                attackTrigger.enabled = true;
            }

            if (attacking)
            {
                if (attackTimer > 0)
                {
                    attackTimer -= Time.deltaTime;
                }
                else
                {
                    attacking = false;
                    attackTrigger.enabled = false;
                }
            }
        }
    }
}