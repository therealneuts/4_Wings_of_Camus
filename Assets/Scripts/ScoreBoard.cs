using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    int score;
    Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText = FindObjectOfType<Text>();
        scoreText.text = score.ToString();
	}

    public void IncrementScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }
}
