using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Char : MonoBehaviour
{
    private Rigidbody pRb;
    private Animator panim;
    public float moveSpeed;
    private float pForward, pStrafe;
    public AudioClip m_audioClip;
    public AudioClip m_audioClip2;
    private AudioSource m_audioSource;
    playerHealth playerHealth;
    [SerializeField] float secondWind = 3;

    public Text secondWindText;
    public bool attacking = false;
    private float attackTimer = 0;
    private float attackswing = 0;
    private float attackSwingCooldown;
    private float attackCoolDown = 1f;
    public Collider attackTrigger;


    void Awake()
    {
        attackTrigger.enabled = false;
    }
    void Start ()
    {
        secondWindText = GameObject.FindGameObjectWithTag("SecondWind").GetComponent<Text>();
        playerHealth = GetComponent<playerHealth>();
        panim = GetComponentInChildren<Animator>();
        pRb = GetComponent<Rigidbody>();
        m_audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(playerHealth.healthBar.value <= 0) { panim.SetLayerWeight(1, 0); CantKill(); return; }
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
                
            }
        }
        
    }


    public void CanKill()
    {
        m_audioSource.panStereo = 1f;
        m_audioSource.pitch = 1f;
        m_audioSource.PlayOneShot(m_audioClip2);
        attackTrigger.enabled = true;
    }

    public void CantKill()
    {
        attackTrigger.enabled = false;
    }


    float counter;

    public void Move(float forward, float strafe, bool jump, bool attack, bool roar, bool run)
    {
        panim.SetFloat("Forward", forward);
        if (run)
        {
            panim.SetBool("Run", true);
            moveSpeed = 2.5f;
            return;
        }
        else
        {
            panim.SetBool("Run", false);
            moveSpeed = 1.5f;
        }
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

            }
        }
        if(roar &&  counter<=0 && secondWind != 0)
        {
            float coolDown = 2.5f;
            counter = coolDown;
            panim.SetTrigger("Roar");
            m_audioSource.panStereo = 1f;
            m_audioSource.pitch = 1f;
            m_audioSource.PlayOneShot(m_audioClip);
            playerHealth.healthBar.value += 300;
            secondWind--;
            secondWindText.text = "Second Wind: " + secondWind;
        }
        counter -= Time.deltaTime;

    }
}
