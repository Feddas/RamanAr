using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Brackeys youtu.be/eJEpeUH1EMg
/// Sebastian Lange youtu.be/4RpVBYW1r5M
/// </summary>
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Generate3dGraph : MonoBehaviour
{
    public int mapWidth = 40;
    public int mapHeight = 10;
    public float noiseScale = 200;

    [Range(0, 32)]
    public int octaves = 8;

    [Range(0, 1)]
    public float persistance = 0.625f;
    public float lacunarity = 2;

    public int seed = 1;
    public Vector2 offset = new Vector2(14.69f, -.2f);

    public float meshHeightMultiplier = 10;

    private MeshFilter meshFilter
    {
        get
        {
            if (_meshFilter == null)
            {
                _meshFilter = this.GetComponent<MeshFilter>();
            }
            return _meshFilter;
        }
    }
    private MeshFilter _meshFilter;

    private MeshCollider meshCollider
    {
        get
        {
            if (_meshCollider == null)
            {
                _meshCollider = this.GetComponent<MeshCollider>();
            }
            return _meshCollider;
        }
    }
    private MeshCollider _meshCollider;

    void Start()
    {
        GenerateMap();
    }

    void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        var meshData = MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier);
        meshFilter.sharedMesh = meshCollider.sharedMesh = meshData.CreateMesh();
    }
}
