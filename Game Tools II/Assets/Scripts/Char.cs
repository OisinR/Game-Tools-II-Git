using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char : MonoBehaviour
{
    private Rigidbody pRb;
    private Animator panim;
    public float moveSpeed;
    private float pForward, pStrafe;
    public AudioClip m_audioClip;
    public AudioClip m_audioClip2;
    private AudioSource m_audioSource;

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
        m_audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        pForward = Input.GetAxis("Vertical") * moveSpeed;
        pStrafe = Input.GetAxis("Horizontal") * moveSpeed;
        Vector3 velocity = pRb.velocity;

        if (counter <= 0)
        {

            if (pForward < 0.1f & pForward > -0.1f)
            {
                transform.position += transform.right * pStrafe * Time.deltaTime * moveSpeed;
            }
            transform.position += transform.forward * pForward * Time.deltaTime * moveSpeed;
            
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

    float counter;

    public void Move(float forward, float strafe, bool jump, bool attack, bool roar, bool run)
    {
        panim.SetFloat("Forward", forward);

        panim.SetFloat("Strafe", strafe);
        if (jump)
        {
            panim.SetTrigger("Jump");
        }
        if (attack)
        {

            if (!attacking)
            {
                int attackPick = Random.Range(0, 2);
                if(attackPick == 0)
                {
                    panim.SetTrigger("Attack");
                }
                else
                {
                    panim.SetTrigger("Attack2");
                }
                attacking = true;
                attackTimer = attackCoolDown;
                m_audioSource.panStereo = 1f;
                m_audioSource.pitch = 1f;
                m_audioSource.PlayOneShot(m_audioClip2);
                attackTrigger.enabled = true;
            }
        }
        if(roar &&  counter<=0)
        {
            float coolDown = 3f;
            counter = coolDown;
            panim.SetTrigger("Roar");
            m_audioSource.panStereo = 1f;
            m_audioSource.pitch = 1f;
            m_audioSource.PlayOneShot(m_audioClip);
        }
        counter -= Time.deltaTime;

    }
}
