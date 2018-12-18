using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour, Ipoolable
{

    private enum NPCstate { chase, patrol, attack };
    private NPCstate pNPCState;
    private NavMeshAgent pAgent;
    private int pCurrentWaypoint;
    private bool pIsPlayerNear;
    public Animator pAnim;
    public bool dead;
    public bool readyToRespawn;
    private float removedFromScene;
    private float deathTimer = 3f;

    public bool readyToPool;

    private Collider pCol;

    detectHit detectHit;

    private Rigidbody pRb;
    //[SerializeField] Manager pManager;
    [SerializeField] Collider hitbox;
    [SerializeField] float pFieldOfView;
    [SerializeField] float pThresholdDistance;
    [SerializeField] Transform[] Waypoints;
    private GameObject player;
    private GameObject Enemy;

    Navmeshfix navMeshFix;

    void Awake()
    {
        OnObjectPooled();
    }

    public void OnObjectPooled()
    {
        readyToPool = false;
        dead = false;
        detectHit = GetComponent<detectHit>();
        pCol = GetComponent<Collider>();
        pAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        pAnim = GetComponent<Animator>();
        pAgent = GetComponent<NavMeshAgent>();
        pRb = GetComponent<Rigidbody>();
        pCol = GetComponent<Collider>();
        navMeshFix = GetComponent<Navmeshfix>();

        removedFromScene = 2f;
        deathTimer = 3f;
        pNPCState = NPCstate.chase;
        pCurrentWaypoint = 0;
        pAgent.enabled = true;
        pAgent.updatePosition = false;
        pAgent.updateRotation = true;
        HandleAnimation();
        
    }


    void Update()
    {
        if (dead)
        {
            if (deathTimer > 0)
            {
                deathTimer -= Time.deltaTime;
            }
            else
            {
                transform.Translate(Vector3.down * Time.deltaTime, Space.World);
                if (removedFromScene > 0)
                {
                    removedFromScene -= Time.deltaTime;
                }
                else
                {
                    gameObject.SetActive(false);
                    readyToPool = true;
                }
            }
            return;
        }
        if (pAgent != null) { pAgent.nextPosition = transform.position; }
        CheckPlayer();
        switch (pNPCState)
        {
            case NPCstate.chase:
                Chase();
                break;
            case NPCstate.patrol:
                Patrol();
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
        if (Vector3.Distance(player.transform.position, transform.position) < 5 && CheckFieldOfView() && CheckOclusion() && pNPCState == NPCstate.chase)
        {
            pNPCState = NPCstate.attack;
            HandleAnimation();
            return;
        }
        
        if (pNPCState == NPCstate.attack && !CheckOclusion())
        {
            pNPCState = NPCstate.chase;
            HandleAnimation();
        }
        
    }

    private void Chase()
    {
        if (pAgent != null) { pAgent.SetDestination(player.transform.position); }

    }

    void Attack()
    {

    }

    private bool CheckFieldOfView()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        Vector3 angle = Quaternion.FromToRotation(transform.forward, direction).eulerAngles;

        if (angle.y > 180.0f) angle.y = 360.0f - angle.y;
        else if (angle.y < -180.0f) angle.y = angle.y + 360.0f;


        if (angle.y < pFieldOfView / 2)
        {
            return true;
        }
        return false;
    }
    bool CheckOclusion()
    {
        RaycastHit hit;
        Vector3 direction = player.transform.position - transform.position;
        if (Physics.Raycast(this.transform.position, direction, out hit, 5.0f))
        {
            if (hit.collider.gameObject == player)
            {
                return true;
            }
        }
        return false;
    }

    private void Patrol()
    {
        //CheckWaypointDistance();
        //pAgent.SetDestination(Waypoints[pCurrentWaypoint].position);
    }

    private void CheckWaypointDistance()
    {
        if (Vector3.Distance(Waypoints[pCurrentWaypoint].position, transform.position) < pThresholdDistance)
        {
            pCurrentWaypoint = (pCurrentWaypoint + 1) % Waypoints.Length;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && CheckFieldOfView())
        {
            pIsPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            pIsPlayerNear = false;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player" && CheckFieldOfView())
        {
           // pManager.DecreaseHealth();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5.0f);

        Gizmos.color = Color.red;
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            Gizmos.DrawRay(transform.position, direction);
        }

        Vector3 rightDirection = Quaternion.AngleAxis(pFieldOfView / 2, Vector3.up) * transform.forward;
        Vector3 leftDirection = Quaternion.AngleAxis(-pFieldOfView / 2, Vector3.up) * transform.forward;
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, rightDirection * 5f);
        Gizmos.DrawRay(transform.position, leftDirection * 5f);
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
