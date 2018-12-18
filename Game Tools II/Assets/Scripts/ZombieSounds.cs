using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSounds : MonoBehaviour {


    [SerializeField] List<AudioClip> zombieMoans = new List<AudioClip>();
    private AudioSource speaker;
    private float cooldown;

	void Start () {
        speaker = GetComponent<AudioSource>();
        cooldown = Random.Range(5, 20);
    }
	

	void Update ()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

		if(cooldown < 0 && !GetComponentInParent<NPC>().dead)
        {
            speaker.pitch = Random.Range(0.5f, 1.0f);
            speaker.PlayOneShot(zombieMoans[Random.Range(0,2)]);
            cooldown = Random.Range(5, 20);
        }


	}
}
