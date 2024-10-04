using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralMesh3 : MonoBehaviour
{
    Mesh _mesh;
    
    Vector3[] _vertices;
    int[] _triangles;

    public float cellSize;
    public Vector3 gridOffset;
    int _gridSize;
    void Awake()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
    }
    void Update()
    {
        MeshData();
        CreateMesh();
    }


    private void MeshData()
    {
        _vertices = new Vector3[4];
        _triangles = new int[6];
        
        float vertexOffset = cellSize * 0.5f;
        
        // Vertices
        _vertices[0] = new Vector3(-vertexOffset, 0, -vertexOffset) + gridOffset;
        _vertices[1] = new Vector3(-vertexOffset, 0, vertexOffset) + gridOffset;
        _vertices[2] = new Vector3(vertexOffset, 0, -vertexOffset) + gridOffset;
        _vertices[3] = new Vector3(vertexOffset, 0, vertexOffset) + gridOffset;
            
        
        //Triangles
        _triangles[0] = 0;
        _triangles[1] = 1;
        _triangles[2] = 2;
        
        _triangles[3] = 2;
        _triangles[4] = 1;
        _triangles[5] = 3;

    }
    private void CreateMesh()
    {
        _mesh.Clear();
        
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();
    }
    
}
