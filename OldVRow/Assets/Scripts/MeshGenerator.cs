﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {
    public static MeshData GenerateTerrainMesh(float [,] heightMap, float heightMultiplier, AnimationCurve _heightCurve, int levelOfDetail)
    {
        AnimationCurve heightCurve = new AnimationCurve(_heightCurve.keys);
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        int meshSimplificationIncrement = (levelOfDetail==0)?1:levelOfDetail * 2;
        int verticesPerLine = ((width - 1) / meshSimplificationIncrement) + 1;
        int vertexIndex = 0;
        MeshData meshData = new MeshData(verticesPerLine, verticesPerLine);

        for (int y = 0; y < height; y+= meshSimplificationIncrement)
        {
            for (int x = 0; x < width; x+= meshSimplificationIncrement)
            {

                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier, topLeftZ - y);

                meshData.uvs[vertexIndex] = new Vector2(x /(float)width, y / (float)height);
                if(y<height-1 && x < width - 1)
                {
                    //Debug.Log("GeneratedMesh");
                    meshData.AddTriangle(vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                    meshData.AddTriangle(vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex+1);
                    //meshData.AddTriangle(vertexIndex, vertexIndex + 1, vertexIndex + width + 1);

                }
                vertexIndex++;
            }
        }
        return meshData;
    }
	
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    public int triangleIndex;
    public MeshData(int MeshWidth, int MeshHeight)
    {
        vertices = new Vector3[MeshWidth * MeshHeight];
        triangles = new int[(MeshWidth - 1) * (MeshHeight - 1) * 6];
        uvs = new Vector2[MeshWidth * MeshHeight];
    }
    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex+1] = b;
        triangles[triangleIndex+2] = c;

        triangleIndex += 3;
    }
    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}
