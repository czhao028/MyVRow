using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator{
    public static Texture2D DrawTextureFromColorMap(Color[] ColorMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.SetPixels(ColorMap);
        texture.Apply();
        return texture;
    }
    public static Texture2D DrawTextureFromHeightMap(float [,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);
        Texture2D texture = new Texture2D(width, height);
        Color[] colourMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }
        texture.SetPixels(colourMap);
        texture.Apply();
        return texture;
    }

}
