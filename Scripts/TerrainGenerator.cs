using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour
{
    public Slider depthSlider;
    public Slider scaleSlider;

    public int depth;

    public int width = 256;
    public int height = 256;

    public float scale;

    public float offSetX = 100f;
    public float offSetY = 100f;

    void Start()
    {
        offSetX = Random.Range(0f, 99999f);
        offSetY = Random.Range(0f, 99999f);
    }

    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        offSetX += Time.deltaTime * 5;

        depth = (int)depthSlider.value;
        scale = (int)scaleSlider.value;
    }

    TerrainData GenerateTerrain (TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y <height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale + offSetX;
        float yCoord = (float)y / height * scale + offSetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
