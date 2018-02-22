using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public GameObject[] prefabs;
    private Transform playerTransform;
    private float spawnZ = -6.0f;
    //private float tileLength = 
    private int numberTilesScreen = 12;
    private float avgLength = 5.0f;


    private Dictionary<GameObject, float> objectZComponents;
    private List<GameObject> listOfTiles;

	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        objectZComponents = new Dictionary<GameObject, float>();
        listOfTiles = new List<GameObject>();

        for (int i = 0; i < prefabs.Length; i++)
        {
            Vector3 prefabSize = prefabs[i].transform.GetChild(0).GetComponent<Renderer>().bounds.size;
            objectZComponents.Add(prefabs[i], prefabSize.z - 1f);
        }

        for (int i = 0; i < numberTilesScreen; i++)
        {
            SpawnTile();
        }
        

    }
	
	// Update is called once per frame
	void Update () {
        if(spawnZ - (avgLength*numberTilesScreen) < playerTransform.position.z)
        {
            SpawnTile();
            DeleteChild();
        }
		
	}

    private void SpawnTile(int prefabIndex = -1)
    {
        int Counter = 0;
        GameObject go = Instantiate(prefabs[Counter]) as GameObject;
        go.transform.SetParent(this.transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += objectZComponents[prefabs[Counter]];
    }
    private void DeleteChild()
    {
        Destroy(transform.GetChild(0).gameObject);
    }
}
