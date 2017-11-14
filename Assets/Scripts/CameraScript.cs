using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public GameObject player;
	// Use this for initialization
	void Start () {
        transform.position = player.transform.position + new Vector3(0f, 1.5f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + new Vector3(0f, 1.5f, 1f);
    }
}
