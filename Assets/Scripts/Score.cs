using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    private float score = 0.0f;

    public Text scoreText;
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = ((int)score).ToString();
    }
}
