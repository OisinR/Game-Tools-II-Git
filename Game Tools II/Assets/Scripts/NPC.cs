using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{

    private enum NPCstate { chase, patrol };
    private NPCstate pNPCState;
    private NavMeshAgent pAgent;
    private int pCurrentWaypoint;
    private bool pIsPlayerNear;
    public Animator pAnim;

    //[SerializeField] Manager pManager;
    [SerializeField] float pFieldOfView;
    [SerializeField] float pThresholdDistance;
    [SerializeField] Transform[] Waypoints;
    [SerializeField] GameObject player;

    void Start()
    {
        //pAnim = GetComponentInChildren<Animator>();
        pNPCState = NPCstate.patrol;
        pAgent = GetComponent<NavMeshAgent>();
        pCurrentWaypoint = 0;
        pAgent.updatePosition = false;
        pAgent.updateRotation = true;
        HandleAnimation();
    }

    void Update()
    {
        pAgent.nextPosition = transform.position;
        CheckPlayer();
        switch (pNPCState)
        {
            case NPCstate.chase:
                Chase();
                break;
            case NPCstate.patrol:
                Patrol();
                break;
            default:
                break;
        }
    }

    void CheckPlayer()
    {
        if (pIsPlayerNear && CheckFieldOfView() && CheckOclusion() && pNPCState == NPCstate.patrol)
        {
            pNPCState = NPCstate.chase;
            HandleAnimation();
            return;
        }
        if (pNPCState == NPCstate.chase && !CheckOclusion())
        {
            pNPCState = NPCstate.patrol;
            HandleAnimation();
        }
    }

    private void Chase()
    {
        Debug.Log("Chasing");
        pAgent.SetDestination(player.transform.position);
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
        Debug.Log("Patrolling");
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
        Vector3 direction = player.transform.position - transform.position;
        Gizmos.DrawRay(transform.position, direction);

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
            pAnim.SetFloat("Forward", 2);
        }
        else
        {
            pAnim.SetFloat("Forward", 1);
        }
    }
}
