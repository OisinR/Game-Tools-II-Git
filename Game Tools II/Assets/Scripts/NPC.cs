using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour, Ipoolable
{
    //Private Variables
    private enum NPCstate { chase, attack };
    private NPCstate pNPCState;
    private NavMeshAgent pAgent;
    private bool pIsPlayerNear;
    private float removedFromScene;
    private float deathTimer = 3f;
    private Collider pCol;
    private GameObject player;
    private GameObject Enemy;

    [SerializeField] Collider hitbox;
    [SerializeField] float pFieldOfView;
    [SerializeField] float pThresholdDistance;
    [SerializeField] Transform[] Waypoints;

    //Public Variables
    public Animator pAnim;
    public bool dead;
    public bool readyToRespawn;
    public bool readyToPool;


    void Awake()
    {
        OnObjectPooled();
    }

    public void OnObjectPooled()                                                        //when pooled, grab everything thats needed
    {
        readyToPool = false;
        dead = false;
        pCol = GetComponent<Collider>();
        pAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        pAnim = GetComponent<Animator>();
        pAgent = GetComponent<NavMeshAgent>();

        pAnim.SetLayerWeight(1, 1);
        removedFromScene = 2f;
        deathTimer = 3f;
        pNPCState = NPCstate.chase;
        pCol.enabled = true;
        pAgent.enabled = true;
        pAgent.updatePosition = false;
        pAgent.updateRotation = true;
        HandleAnimation();
        
    }


    void Update()
    {
        if (dead)                                                                                           //when killed, play animation and wait for it to finish
        {
            if (deathTimer > 0)
            {
                deathTimer -= Time.deltaTime;
            }
            else
            {
                transform.Translate(Vector3.down * Time.deltaTime, Space.World);                            //after animation is done, sink through the floor and out of sight
                if (removedFromScene > 0)
                {
                    removedFromScene -= Time.deltaTime;                                                     //wait unitl completely gone
                }
                else
                {
                    gameObject.SetActive(false);                                                            //then send it back into the pool
                    readyToPool = true;
                }
            }
            return;
        }
        if (pAgent != null) { pAgent.nextPosition = transform.position; }                                   //makes sure the navmesh agent is there
        CheckPlayer();
        switch (pNPCState)
        {
            case NPCstate.chase:
                Chase();
                break;
            case NPCstate.attack:
                Attack();
                break;
            default:
                break;
        }
    }



    void CheckPlayer()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 1f  && pNPCState == NPCstate.chase)           //if the enemy is close to the player, attack
        {
            pNPCState = NPCstate.attack;
            HandleAnimation();
            return;
        }

        if (Vector3.Distance(player.transform.position, transform.position) > 1.2f && pNPCState == NPCstate.attack)         //otherwise chase
//a bit of leniency to make sure the enemy doesnt keep flickering between states ^
        {
            pNPCState = NPCstate.chase;
            HandleAnimation();
        }
        
    }

    private void Chase()
    {
        if (pAgent != null) { pAgent.SetDestination(player.transform.position); }
        pAnim.applyRootMotion = true;
        pAnim.SetBool("Attack", false);
    }

    void Attack()
    {
        pAnim.applyRootMotion = false;                                                          //when near player, stop root motion an play attack animations
        pAnim.SetBool("Attack", true);
    }

    void HandleAnimation()
    {
        pAgent.nextPosition = transform.position;
        if (pNPCState == NPCstate.chase)
        {
            pAnim.SetBool("Attack", false);
            pAnim.SetFloat("Forward", 2);
        }
        else
        {
            pAnim.SetBool("Attack", false);
            pAnim.SetFloat("Forward", 1);
        }
        if(pNPCState == NPCstate.attack)
        {
            pAnim.SetFloat("Forward", 0);
            pAnim.SetBool("Attack", true);
        }
    }


}
