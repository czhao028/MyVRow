using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {
    public static void GenerateTerrainMesh(float [,] heightMap)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        int topLeftX = (width - 1) / -2f;
        int topLeftZ = (height - 1) / 2f; 

        int vertexIndex = 0;
        MeshData mesh = new MeshData(width, height);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                mesh.vertices[vertexIndex] = new Vector3(topLeftX + x, heightMap[x, y], topLeftZ - y);
                mesh.AddTriangle(vertexIndex, vertexIndex + width, vertexIndex + width + 1);
                mesh.AddTriangle(vertexIndex, vertexIndex + 1, vertexIndex + width + 1);
            }
        }
    }
	
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;

    public int triangleIndex;
    public MeshData(int MeshWidth, int MeshHeight)
    {
        vertices = new Vector3[MeshWidth * MeshHeight];
        triangles = new int[(MeshWidth - 1) * (MeshHeight - 1) * 6];
    }
    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex+1] = b;
        triangles[triangleIndex+2] = c;

        triangleIndex += 3;
    }
}
