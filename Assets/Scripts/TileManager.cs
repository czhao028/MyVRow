using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public GameObject[] prefabs;
    private int upStairsUpperBound;
    private Transform playerTransform;
    private float spawnZ = 10.0f;
    private int numberTilesScreen = 50;
    private float avgLength = 5.0f;
    private float safeZone = 15.0f;

    private Dictionary<GameObject, float> objectZComponents;
    private Queue<int> queue;

    private int[] upIndexes = new int[] { 0, 1, 4 };

	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        objectZComponents = new Dictionary<GameObject, float>();
        queue = new Queue<int>();

        //upStairsUpperBound = Mathf.FloorToInt((float)prefabs.Length / 2.0f);
        //Debug.Log(upStairsUpperBound);

        for (int i = 0; i < prefabs.Length; i++)
        {
            Vector3 prefabSize = prefabs[i].transform.GetChild(0).GetComponent<Renderer>().bounds.size;
            objectZComponents.Add(prefabs[i], prefabSize.z);
        }
        for (int i = 0; i < numberTilesScreen; i++)
        {
            SpawnTileHelper();
        }
    }
	
	void Update () {
        if(safeZone + spawnZ - (avgLength*numberTilesScreen) < playerTransform.position.z)
        {
            SpawnTile();
            DeleteChild();
        }
	}

    private void SpawnTile()
    {
        if(queue.Count != 0)
        {
            SpawnTileHelper(queue.Dequeue());
        }
        else
        {
            int index = upIndexes[Random.Range(0, upIndexes.Length)];
            Debug.Log(index + "UpIndex");
            SpawnTileHelper(index);
            for (int i = 0; i < Random.Range(5, 20); i++) { queue.Enqueue(index + 1); Debug.Log("Queued" + index + 1); }
        
            queue.Enqueue(index+2);
        }
        
    }

    private void SpawnTileHelper(int prefabIndex = 0)
    {
        int Counter = prefabIndex;
        GameObject go = Instantiate(prefabs[Counter]) as GameObject;
        go.transform.SetParent(this.transform);
        go.transform.position = (Vector3.forward * spawnZ);
        spawnZ += objectZComponents[prefabs[Counter]] - 1f;
    }
    private void DeleteChild()
    {
        Destroy(transform.GetChild(0).gameObject);
    }

    /***
     * Plan:
     * 1. If Queue NOT EMPTY: pop the first one off 
     * 2. If Queue EMPTY:
     *  a. while(True): Generate a random number
     *      i. if number == 0: spawn Tile
     *  b. Get the GameObject, prefabs[Random]
     *      i. if the name of GameObject contains Up - spawn Tile functions, add Random.Range(0,20) of prefabs[Random+1] to Queue & prefabs[Random+2]
     *      ii. else: continue
     * 
     * 
     ***/
}

//public class TileManager : MonoBehaviour
//{

//    public GameObject[] prefabs;
//    private Transform playerTransform;
//    private float spawnZ = -6.0f;
//    //private float tileLength = 
//    private int numberTilesScreen = 12;
//    private float avgLength = 5.0f;


//    private Dictionary<GameObject, float> objectZComponents;
//    private List<GameObject> listOfTiles;

//    void Start()
//    {
//        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
//        objectZComponents = new Dictionary<GameObject, float>();
//        listOfTiles = new List<GameObject>();

//        for (int i = 0; i < prefabs.Length; i++)
//        {
//            Vector3 prefabSize = prefabs[i].transform.GetChild(0).GetComponent<Renderer>().bounds.size;
//            objectZComponents.Add(prefabs[i], prefabSize.z - 1f);
//        }

//        for (int i = 0; i < numberTilesScreen; i++)
//        {
//            SpawnTile();
//        }


//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (spawnZ - (avgLength * numberTilesScreen) < playerTransform.position.z)
//        {
//            SpawnTile();
//            DeleteChild();
//        }

//    }

//    private void SpawnTile(int prefabIndex = -1)
//    {
//        int Counter = 0;
//        GameObject go = Instantiate(prefabs[Counter]) as GameObject;
//        go.transform.SetParent(this.transform);
//        go.transform.position = Vector3.forward * spawnZ;
//        listOfTiles.Add(go);
//        spawnZ += objectZComponents[prefabs[Counter]];
//    }
//    private void DeleteChild()
//    {
//        Destroy(listOfTiles[0]);
//        listOfTiles.RemoveAt(0);
//    }
