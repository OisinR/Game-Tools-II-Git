using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char : MonoBehaviour
{
    private Rigidbody pRb;
    private Animator panim;
    public float moveSpeed;
    private float pForward, pStrafe;

    public bool attacking = false;
    private float attackTimer = 0;
    public float attackCoolDown = 0.3f;
    public Collider attackTrigger;


    void Awake()
    {
        attackTrigger.enabled = false;
    }
    void Start ()
    {
        panim = GetComponentInChildren<Animator>();
        pRb = GetComponent<Rigidbody>();
	}

    private void Update()
    {
        pForward = Input.GetAxis("Vertical") * moveSpeed;
        pStrafe = Input.GetAxis("Horizontal") * moveSpeed;
        Vector3 velocity = pRb.velocity;
        
        if (pForward <0.1f & pForward >-0.1f)
        {
            velocity.x = pStrafe;
        }
        velocity.z = pForward;
        pRb.velocity = velocity;

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

    public void Move(float forward, float strafe, bool jump, bool attack)
    {
        panim.SetFloat("Forward", forward);

        panim.SetFloat("Strafe", strafe);
        if (jump)
        {
            panim.SetTrigger("Jump");
        }
        if (attack)
        {
            panim.SetTrigger("Attack");

            if (!attacking)
            {
                attacking = true;
                attackTimer = attackCoolDown;

                attackTrigger.enabled = true;
            }
        }
    }
}
