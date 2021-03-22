using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Random = UnityEngine.Random;

public class PerlinNoise : MonoBehaviour
{
    public int width = 256;
    public int height = 256;

    public float scale = 20;

    public float offSetx = 100f;
    public float offSety = 100f;

    void Start()
    {
        offSetx = Random.Range(0f, 99999f);
        offSety = Random.Range(0f, 99999f);
    }
    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    Color CalculateColor(int x, int y)
    {

        float xCoord = (float)x / width * scale + offSetx;
        float yCoord = (float)y / height * scale + offSety;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
    }
}
