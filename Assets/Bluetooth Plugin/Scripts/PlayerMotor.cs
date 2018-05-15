using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {
    
    private CharacterController controller;
    public float speed;
    private Vector3 moveForward;

    private float animationDuration = 3.0f;
    void Start () {
        controller = GetComponent<CharacterController>();
	}
	void Update () {
        if(Time.time < animationDuration)
        {
            controller.Move(Vector3.forward * Time.deltaTime * speed);
            return;
        }
        moveForward = Vector3.zero; //reset
        moveForward.x = Input.GetAxis("Horizontal") * speed; //no left/right movement
        if (!controller.isGrounded)
        {
            moveForward += Physics.gravity; //up down
        }
        moveForward.z = speed; //forward back; SUBSTITUTE speed for speed read in from the PM5 monitor
        controller.Move(moveForward * Time.deltaTime);
        //Debug.Log(controller.transform.position);
 
	}
}
