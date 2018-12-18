using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectFoot : MonoBehaviour {

    Footfall footfall;

    void Start () {
        footfall = GetComponentInChildren<Footfall>();
	}
	
	// Update is called once per frame
	public void SendMessage(int f)
    {
        //footfall.MakeFootprint(f);
	}
}
