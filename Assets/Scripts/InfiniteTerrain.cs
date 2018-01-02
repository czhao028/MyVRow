using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteTerrain : MonoBehaviour {

    public const float maxViewDst = 450;
    public Transform viewer;
    public Material mapMaterial;

    static MapGenerator mapGenerator;
    public static Vector2 viewerPosition;

    int chunkSize;
    int chunksVisibleInViewDst;

    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>();
    void Start()
    {
        mapGenerator = FindObjectOfType<MapGenerator>();
        chunkSize = MapGenerator.mapChunkSize - 1;
        chunksVisibleInViewDst = Mathf.RoundToInt(maxViewDst / chunkSize);

    }
    void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
        //Debug.Log(viewerPosition);
        UpdateVisibleChunks();
    }
    void UpdateVisibleChunks()
    {
        for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++)
        {
            terrainChunksVisibleLastUpdate[i].SetVisible(false);
        }
        terrainChunksVisibleLastUpdate.Clear();

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);
        for (int yOffSet = -chunksVisibleInViewDst; yOffSet < chunksVisibleInViewDst; yOffSet++)
        {
            for (int xOffSet = -chunksVisibleInViewDst; xOffSet < chunksVisibleInViewDst; xOffSet++)
            {
                Vector2 coord = new Vector2(currentChunkCoordX + xOffSet, currentChunkCoordY + yOffSet);
                if (terrainChunkDictionary.ContainsKey(coord))
                {
                    terrainChunkDictionary[coord].UpdateTerrainChunk();
                    if (terrainChunkDictionary[coord].IsVisible())
                    {
                        terrainChunksVisibleLastUpdate.Add(terrainChunkDictionary[coord]);
                    }
                    //Debug.Log("ContainsKey");
                }
                else
                {
                    terrainChunkDictionary.Add(coord, new TerrainChunk(coord, chunkSize,transform,mapMaterial));
                    //Debug.Log("DoesntContainKey");
                }
            }
        }
    }
    public class TerrainChunk
    {
        public GameObject meshObject;
        Vector2 position;
        Bounds bounds;

        MeshRenderer meshRenderer;
        MeshFilter meshFilter;

        public TerrainChunk(Vector2 coordinates, int size, Transform parent, Material material)
        {
            position = coordinates * size;
            bounds = new Bounds(position, Vector2.one * size);

            Vector3 v3position = new Vector3(position.x, 0, position.y);
            meshObject = new GameObject("Terrain Chunk");
            meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshFilter =  meshObject.AddComponent<MeshFilter>();
            meshRenderer.material = material;

            meshObject.transform.position = v3position;
            meshObject.transform.parent = parent;
            SetVisible(false);

            mapGenerator.RequestMapData(OnMapDataReceived);

        }
        void OnMapDataReceived(MapData mapData)
        {
            mapGenerator.RequestMeshData(mapData, OnMeshDataReceived);
        }

        void OnMeshDataReceived(MeshData meshData)
        {
            meshFilter.mesh = meshData.CreateMesh();
        }
        public void UpdateTerrainChunk()
        {
            float distancefromViewer = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
            bool update = distancefromViewer <= maxViewDst;
            SetVisible(update);
        }
        public void SetVisible(bool visible)
        {
            meshObject.SetActive(visible);
        }
        public bool IsVisible()
        {
            return meshObject.activeSelf;
        }
    }

}
