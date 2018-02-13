using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {
    private Transform playerTransform;
    private Vector3 offset;

    private int animationDuration = 3;
    private float transition = 0.0f;

    private Vector3 moveVector;
    private Vector3 animationOffset = new Vector3(0, 5, 5);
    void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - playerTransform.position;
	}
	
	// Update is called once per frame
	void Update () {
        moveVector = playerTransform.position + offset;
        moveVector.x = 0;
        moveVector.y = Mathf.Clamp(moveVector.y, 3, 5);
        if(transition >= 1.0f)
        {
            transform.position = moveVector;
        }
        else
        {
            transform.position = Vector3.Lerp(moveVector, moveVector + animationOffset, transition);

        }
        
        
	}
}
