using System;
using Unity.Mathematics;
using UnityEngine;

namespace MyTerrain
{
    public class ColoringVertices : MonoBehaviour
    {
        Mesh _mesh;
        MeshFilter _meshFilter;
        MeshTopology _meshTopology;
        float4 n = float4.zero;

        private void Awake()
        {
            _mesh = GetComponent<MeshFilter>().mesh;
            _meshTopology = gameObject.GetComponent<MeshTopology>();

            _meshTopology = MeshTopology.Points;
        }
    }
}