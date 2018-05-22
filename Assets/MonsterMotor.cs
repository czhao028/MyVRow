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
    public Animator anim;

    void Start () {
        //PlayerMotor playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        //monsterSpeed = playerTransform.speed;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        monsterMove = new Vector3(0, 0, monsterSpeed);
    }

    //void OnAnimatorMove()
    //{
    //    if (anim)
    //    {
    //        Vector3 newPosition = transform.position;
    //        anim.Play("Walk Mec");
    //        anim.SetFloat(Animator.StringToHash("VerticalMove"), 0.05f);
    //        newPosition += new Vector3(0, 0, anim.GetFloat("VerticalMove"));
    //        transform.position = newPosition;
    //    }
    //}

    // Update is called once per frame
    void Update () {
        if (isDead)
        {
            return;
        }
        //if (!controller.isGrounded)
        //{
        //    monsterMove += Physics.gravity; //up down
        //}
        //transform.position += (monsterMove * Time.deltaTime);
        //anim.Play("creature1walkforward");
        //Debug.Log("PlayedCreatureClip");
        //anim.speed = 5;
        //anim.AddClip(walkClip, "WalkClip");
        //anim.SetFloat("Speed", 1f);
        //Debug.Log(anim.GetFloat("Speed"));
   
        //controller.Move(monsterMove * Time.deltaTime);
        Vector3 newPosition = transform.position;
        anim.Play("Walk Mec");
        anim.SetFloat(Animator.StringToHash("VerticalMove"), 0.05f);
        newPosition += new Vector3(0, 0, anim.GetFloat("VerticalMove"));
        transform.position = newPosition;

        //anim.Play("Run_Mech");
        //Debug.Log("PlayedCreatureClip");
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
