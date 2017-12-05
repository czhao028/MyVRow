using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteTerrain : MonoBehaviour {

    public const float maxViewDst = 300;
    public Transform viewer;

    public static Vector2 viewerPosition;

    int chunkSize;
    int chunksVisibleInViewDst;

    void Start()
    {
        chunkSize = MapGenerator.mapChunkSize - 1;
        chunksVisibleInViewDst = Mathf.RoundToInt(maxViewDst / chunkSize);

    }
    void UpdateVisibleChunks()
    {
        Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);
        for (int yOffset = -chunksVisibleInViewDst; yOffset < chunksVisibleInViewDst; yOffset++)
        {
            for (int xOffSet = -chunksVisibleInViewDst; xOffSet < chunksVisibleInViewDst; xOffSet++)
            {
                Vector2 coord = new Vector2(currentChunkCoordX + xOffSet, currentChunkCoordY + yOffSet);
                if (terrainChunkDictionary.ContainsKey(coord))
                {

                }
                else
                {
                    terrainChunkDictionary.Add(coord, new TerrainChunk());
                }
            }
        }
    }

    public class TerrainChunk
    {
        public GameObject plane;
        Vector2 position;
        public TerrainChunk(Vector2 coordinates, int size)
        {
            position = coordinates * size;
            plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            //plane.transform
        }
    }

}
