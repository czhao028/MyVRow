using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMotor : MonoBehaviour {

    public float monsterSpeed;
    private CharacterController controller;
    public Vector3 monsterMove;
    // Use this for initialization
    void Start () {
        //PlayerMotor playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        //monsterSpeed = playerTransform.speed;
        controller = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!controller.isGrounded)
        {
            monsterMove += Physics.gravity; //up down
        }
        monsterMove.z = monsterSpeed;
        controller.Move(monsterMove);

	}
}
