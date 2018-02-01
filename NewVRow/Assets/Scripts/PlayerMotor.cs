using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {
    private CharacterController controller;
    //SUBSTITUTE speed for speed read in from the PM5 monitor
    public float speed;
	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        controller.Move((Vector3.forward*speed) * Time.deltaTime);
        Debug.Log(controller.transform.position);
	}
}
