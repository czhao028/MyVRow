using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {
    private CharacterController controller;
    public float speed;
    private Vector3 moveForward;
	void Start () {
        controller = GetComponent<CharacterController>();
	}
	void Update () {
        moveForward = Vector3.zero;

        moveForward.x = 0;
        if (!controller.isGrounded)
        {
            moveForward.y = -0.5f;
        }
        moveForward.z = 1.0f * speed; //SUBSTITUTE speed for speed read in from the PM5 monitor
        controller.Move(moveForward * Time.deltaTime);
        Debug.Log(controller.transform.position);
	}
}
