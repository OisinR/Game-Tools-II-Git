﻿using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;

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

    public bool clickykilly;


    private void Start()
    {
        GetComponents();
    }


    void GetComponents()
    {
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
            anim.SetTrigger("Dead");
            pDead = true;
            hitbox.enabled = false;
            Destroy(pRb);
            pCol.enabled = false;
            pAgent.enabled = false;
            healthbar = 100;

        }
    }

    void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag != opponent) return;

		healthbar -= 100;

		
	}


}
