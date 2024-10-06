using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralMesh7 : MonoBehaviour
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
        ContiguousProceduralGrid();
        CreateMesh();
    }


    private void ContiguousProceduralGrid()
    {
        _vertices = new Vector3[(gridSize + 1) * (gridSize + 1)];
        _triangles = new int[gridSize * gridSize * 6];
        
        float vertexOffset = cellSize * 0.5f;
        int v = 0;
        int t = 0;

        // Vertices
        for (int x = 0; x <= gridSize; x++)
        {
            for (int y = 0; y <= gridSize; y++)
            {
                _vertices[v] = new Vector3(x * cellSize - vertexOffset, 0, y * cellSize - vertexOffset);
                v++;
            }
        }

        v = 0;
        
        //Triangles
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                _triangles[t] = v;
                _triangles[t + 1] = v + 1;
                _triangles[t + 2] = v + gridSize + 1;

                _triangles[t + 3] = v + gridSize + 1;
                _triangles[t + 4] = v + 1;
                _triangles[t + 5] = v + gridSize + 2;
                
                v++;
                t += 6;
            }
            v++;
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
