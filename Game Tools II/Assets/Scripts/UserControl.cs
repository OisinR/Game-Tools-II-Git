using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControl : MonoBehaviour
{
    private float pStrafe, pForward;
    private Char pChar;
    private bool pJump, pAttack,pRoar,pRun;

	void Start ()
    {
        pChar = GetComponent<Char>();
	}
	
	private void FixedUpdate()
    {
        pStrafe = Input.GetAxis("Horizontal");
        pForward = Input.GetAxis("Vertical");
        pJump = Input.GetButtonDown("Jump");
        pAttack = Input.GetButtonDown("Fire1");
        pRoar = Input.GetKeyDown(KeyCode.R);
        pRun = Input.GetKey(KeyCode.LeftShift);
        pChar.Move(pForward,pStrafe,pJump,pAttack,pRoar,pRun);
	}
}
