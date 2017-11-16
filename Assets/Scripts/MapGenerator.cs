using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public int mapWidth;
    public int mapHeight;
    public float scale;
    public bool autoUpdate;
    public int octaves;
    public float persistance;
    public float lacunarity;

    public int seed;

	// Use this for initialization
    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, scale, octaves, persistance, lacunarity);

        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
