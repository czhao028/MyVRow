using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public GameObject[] prefabs;

    private Transform playerTransform;
    private float spawnZ = 0.0f;
    //private float tileLength = 

	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < prefabs.Length; i++)
        {
      
            foreach(Transform child in prefabs[i].transform)
            {
                Debug.Log(child.GetComponent<Collider>().bounds.size);
            }
            
        }
		
	}
}
