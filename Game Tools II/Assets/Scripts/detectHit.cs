using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class detectHit : MonoBehaviour {

	public float healthbar = 100;
	Animator anim;
    NPC npc;
	public string opponent;
    public bool pDead;
    private Rigidbody pRb;
    private chase pChase;
    private Collider pCol;
    public Collider hitbox;
    private NavMeshAgent pAgent;
    private bool isNPC;

    public GameObject blood;

    ObjectPooler objectPooler;
    [SerializeField] string[] tag;


    Score score;

    public bool clickykilly;

    [SerializeField] List<AudioClip> deathMoans = new List<AudioClip>();

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
        pChase = GetComponent<chase>();
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
        if ((healthbar < 1 && !pDead) | clickykilly)
        {
            GetComponents();
            if (isNPC)
            {
                npc.dead = true;
            }
            anim.SetLayerWeight(1,0); 
            anim.SetTrigger("Dead");
            speaker.pitch = Random.Range(0.5f, 1.0f);
            speaker.PlayOneShot(deathMoans[Random.Range(0, 2)]);
            pDead = true;
            hitbox.enabled = false;
            Destroy(pRb);
            pCol.enabled = false;
            pAgent.enabled = false;
            healthbar = 100;
            score.AddScore();
        }
    }

    void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag != opponent) return;
        objectPooler.SpawnFromPool(tag[0], transform.position, transform.rotation);
        //Instantiate(blood, transform.position, Quaternion.identity);
		healthbar -= 100;

		
	}


}
