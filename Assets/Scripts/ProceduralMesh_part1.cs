using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralMesh1 : MonoBehaviour
{
    Mesh _mesh;
    
    Vector3[] _vertices;
    int[] _triangles;
    
    void Start()
    {
        _mesh = GetComponent<MeshFilter>().mesh;

        MeshData();
        CreateMesh();
    }


    private void MeshData()
    {
        // Vertices
        _vertices = new Vector3[]{new(0,0,0), new(0,0,1), new(1, 0, 0)};
        
        //Triangles
        _triangles = new[]{0, 1, 2};
    }
    private void CreateMesh()
    {
        _mesh.Clear();
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
    }
    
}
