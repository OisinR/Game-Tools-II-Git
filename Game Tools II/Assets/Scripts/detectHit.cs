using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class detectHit : MonoBehaviour {


    //Private Variables
    Score score;
    Animator anim;
    NPC npc;
    Rigidbody pRb;
    Collider pCol;
    NavMeshAgent pAgent;
    bool isNPC;
    ObjectPooler objectPooler;
    [SerializeField] string[] tagg;
    [SerializeField] List<AudioClip> deathMoans = new List<AudioClip>();

    //Public Variables
    public float healthbar = 100;
    public string opponent;
    public bool pDead;
    public Collider hitbox;
    public AudioSource speaker;


    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        GetComponents();
    }


    void GetComponents()
    {
        score = GameObject.FindGameObjectWithTag("Manager").GetComponent<Score>();
        pAgent = GetComponent<NavMeshAgent>();
        pRb = GetComponent<Rigidbody>();
        pCol = GetComponent<Collider>();
        anim = GetComponentInChildren<Animator>();
        if (GetComponent<NPC>() != null)
        {
            npc = GetComponent<NPC>();
            isNPC = true;
        }
    }

    private void FixedUpdate()
    {
        if ((healthbar < 1 && !pDead))
        {
            GetComponents();                                                        //re-gets components every time its spawned
            if (isNPC)
            {
                npc.dead = true;
            }
            anim.SetLayerWeight(1,0);                                                               //stops zombie swinging if it died mid attack
            anim.SetTrigger("Dead");                
            speaker.pitch = Random.Range(0.5f, 1.0f);                                                   //gives random pitch and chooses from one of the death moans
            speaker.PlayOneShot(deathMoans[Random.Range(0, 2)]);
            pDead = true;                                                                       //disables most of the components so the dead body doesnt interfear with anything
            hitbox.enabled = false;
            Destroy(pRb);                                                                       //in order to sink through the floor, the rigidbody has to go
            pCol.enabled = false;
            pAgent.enabled = false;
            healthbar = 100;                                                                    //reset health bar for next spawn
            score.AddScore();                                                                   //add to the score
        }
    }

    void OnTriggerEnter(Collider other)
	{                                                                                           //if attacked by the player, spawn in some blood
		if(other.gameObject.tag != opponent) return;
        objectPooler.SpawnFromPool(tagg[0], transform.position, transform.rotation);
		healthbar -= 100;

		
	}


}
