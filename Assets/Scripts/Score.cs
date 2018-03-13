using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    private float score = 0.0f;
    public Text scoreText;
    private int scoreToNextLevel = 15;
    private int difficultyLevel = 1;
    private int maxLevel = 10;

    private bool isDead = false;

	void Start () {
        scoreText.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
        if (isDead)
        {
            return;
        }
        if (difficultyLevel >= maxLevel) { }
        else if (score >= scoreToNextLevel)
        {
            scoreToNextLevel *= 2;
            difficultyLevel += 1;
        }
        score += Time.deltaTime * difficultyLevel;
        scoreText.text = ((int)score).ToString();
    }

    public void OnDeath()
    {
        isDead = true;
    }
}
