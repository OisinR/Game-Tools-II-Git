using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text scoreText;
    public float score = 0;

    void Start ()
    {
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
    }
	
	public void AddScore()
    {
        score += 100;
        scoreText.text = "Score: " + score;
    }
}
