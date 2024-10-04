using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralMesh4 : MonoBehaviour
{
    Mesh _mesh;
    
    Vector3[] _vertices;
    int[] _triangles;

    public float cellSize;
    public Vector3 gridOffset;
    public int gridSize;
    void Awake()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
    }
    void Update()
    {
        DiscreteProceduralGrid();
        CreateMesh();
    }


    private void DiscreteProceduralGrid()
    {
        _vertices = new Vector3[gridSize * gridSize * 4];
        _triangles = new int[gridSize * gridSize * 6];
        
        float vertexOffset = cellSize * 0.5f;
        int v = 0;
        int t = 0;

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                Vector3 cellOffset = new Vector3(x * cellSize, 0, y * cellSize);

                // Vertices
                _vertices[v] = new Vector3(-vertexOffset, 0, -vertexOffset) + cellOffset + gridOffset;
                _vertices[v + 1] = new Vector3(-vertexOffset, 0, vertexOffset) + cellOffset + gridOffset;
                _vertices[v + 2] = new Vector3(vertexOffset, 0, -vertexOffset) + cellOffset + gridOffset;
                _vertices[v + 3] = new Vector3(vertexOffset, 0, vertexOffset) + cellOffset + gridOffset;
                
                
                

                //Triangles
                _triangles[t] = v;
                _triangles[t + 1] = v + 1;
                _triangles[t + 2] = v + 2;

                _triangles[t + 3] = v + 2;
                _triangles[t + 4] = v + 1;
                _triangles[t + 5] = v + 3;

                v += 4;
                t += 6;
            }
        }
    }
    private void CreateMesh()
    {
        _mesh.Clear();
        
        _mesh.SetVertices(_vertices);
        _mesh.SetTriangles(_triangles, 0);
        _mesh.RecalculateNormals();
    }
    
}
