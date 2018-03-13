using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMotor : MonoBehaviour {

    public float monsterSpeed;
    private CharacterController controller;
    public Vector3 monsterMove;
    private bool isDead = false;
    public AnimationClip deathClip;
    public AnimationClip walkClip;
    public Animation anim;

    void Start () {
        //PlayerMotor playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        //monsterSpeed = playerTransform.speed;
        controller = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isDead)
        {
            return;
        }
        if (!controller.isGrounded)
        {
            monsterMove += Physics.gravity; //up down
        }
        monsterMove.z = monsterSpeed;
        controller.Move(monsterMove);
	}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            Debug.Log("I hit a player");
            Death();
        }
    }
    private void Death()
    {
        isDead = true;
        Debug.Log("Death");
    }
}
