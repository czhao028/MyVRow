using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteTerrain : MonoBehaviour {

    public LODInfo[] lodinfos;
    public static float maxViewDst;

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

        maxViewDst = lodinfos[lodinfos.Length - 1].maxViewThreshold;

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
                    Debug.Log("ContainsKey");
                }
                else
                {

                    terrainChunkDictionary.Add(coord, new TerrainChunk(coord, chunkSize,transform,mapMaterial, lodinfos));

                    Debug.Log("DoesntContainKey");
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


        LODInfo[] lodinfos;
        LODMesh[] lodmeshes;

        MapData mapData;
        bool mapDataReceived = false;

        int previousLODIndex = -1;

        public TerrainChunk(Vector2 coordinates, int size, Transform parent, Material material, LODInfo[] lodinfos)
        {
            this.lodinfos = lodinfos;
            position = coordinates * size;
            bounds = new Bounds(position, Vector2.one * size);

            Vector3 v3position = new Vector3(position.x, 0, position.y);
            meshObject = new GameObject("Terrain Chunk");
            meshRenderer = meshObject.AddComponent<MeshRenderer>();

            meshFilter = meshObject.AddComponent<MeshFilter>();


            meshRenderer.material = material;

            meshObject.transform.position = v3position;
            meshObject.transform.parent = parent;
            SetVisible(false);


            lodmeshes = new LODMesh[lodinfos.Length];
            for (int i = 0; i < lodmeshes.Length; i++)
            {
                lodmeshes[i] = new LODMesh(lodinfos[i].lod);
            }

            mapGenerator.RequestMapData(OnMapDataReceived);

        }
        void OnMapDataReceived(MapData mapData)
        {

            this.mapData = mapData;
            mapDataReceived = true;
            //mapGenerator.RequestMeshData(mapData, OnMeshDataReceived);
        }

        void OnMeshDataReceived(MeshData meshData)
        {
            meshFilter.mesh = meshData.CreateMesh();

        }

        public void UpdateTerrainChunk()
        {
            //find distance from viewer to chunk; goes through LODInfo thresholds until it hits in range, index that respective mesh in LODMesh
            if (mapDataReceived)
            {
                float distancefromViewer = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
                bool update = distancefromViewer <= maxViewDst;
                if (update)
                {
                    int counter = 0;
                    while (distancefromViewer > lodinfos[counter].maxViewThreshold)
                    {
                        counter++;
                    }
                    if (counter != previousLODIndex) //AND lodmesh[counter].hasMesh == False 
                    {
                        LODMesh lodmesh = lodmeshes[counter];
                        if (lodmesh.hasMesh)
                        {
                            previousLODIndex = counter;
                            meshFilter.mesh = lodmesh.mesh;
                        }
                        else if (!lodmesh.hasRequestedMesh)
                        {

                            lodmesh.RequestMesh(this.mapData);
                        }
                        

                        //the mesh = lodmeshes[counter].RequestMesh(this.mapData);

                        //DO SOMETHING WITH mesh, already in LODMesh object
                    }
                    SetVisible(update);
                }
            }
            
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

    class LODMesh
    {
        public Mesh mesh;
        public bool hasRequestedMesh;
        public bool hasMesh;
        int lod;

        public LODMesh(int lod)
        {
            this.lod = lod;
        }
        void OnMeshDataReceived(MeshData meshData)
        {
            mesh = meshData.CreateMesh();
            hasMesh = true;
        }
        public void RequestMesh(MapData mapData)
        {
            hasRequestedMesh = true;
            mapGenerator.RequestMeshData(mapData, lod, OnMeshDataReceived);
        }
    }

    [System.Serializable]
    public struct LODInfo
    {
        public int lod;
        public float maxViewThreshold;
    }

}
