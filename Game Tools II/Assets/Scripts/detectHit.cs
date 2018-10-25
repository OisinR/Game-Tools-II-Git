using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;

public class detectHit : MonoBehaviour {

	public float healthbar = 100;
	Animator anim;
	public string opponent;
    private bool pDead;
    private Rigidbody pRb;
    private chase pChase;
    private Collider pCol;
    public Collider hitbox;
    private NavMeshAgent pAgent;

    void Start()
    {
        pAgent = GetComponent<NavMeshAgent>();
        pRb = GetComponent<Rigidbody>();
        pChase = GetComponent<chase>();
        pCol = GetComponent<Collider>();
        anim = GetComponentInChildren<Animator>();
    }
    private void FixedUpdate()
    {
        if (healthbar <= 0 && !pDead)
        {
            anim.SetTrigger("Dead");
            pDead = true;
            Destroy(hitbox);
            Destroy(pRb);
            Destroy(pChase);
            Destroy(pCol);
            Destroy(pAgent);
        }
    }

    void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag != opponent) return;

		healthbar -= 100;

		
	}


}
