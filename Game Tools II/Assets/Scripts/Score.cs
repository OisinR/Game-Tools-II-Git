using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text scoreText;
    public float score = 0;

    void Start ()
    {
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();             //grabs the score
    }
	
	public void AddScore()
    {
        score += 100;                                                                           //adds to the score when the function is called
        scoreText.text = "Score: " + score;
    }
}
