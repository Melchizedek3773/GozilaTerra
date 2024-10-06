using UnityEngine;

namespace MyTerrain
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))] 
    public class TerrainGenerator : MonoBehaviour
    {
        [SerializeField] float cellSize;
        [SerializeField] int gridSize;
        
        [SerializeField] Gradient terrainGradient;
        [SerializeField] Material mat;
        
        private Vector3[] _vertices;
        private int[] _triangles;
        private Texture2D _gradientTexture;
        
        Mesh _mesh;
        MeshCollider _meshCollider;
        void Awake()
        {
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;
            _mesh.name = "Terrain";
            _meshCollider = GetComponent<MeshCollider>();
            
            ContiguousProceduralGrid();
            CreateMesh();
            
            _meshCollider.sharedMesh = _mesh;
            
            // Uv
            Vector2[] uvs = new Vector2[_vertices.Length];

            for (int i = 0; i < uvs.Length; i++)
            {
                uvs[i] = new Vector2(_vertices[i].x, _vertices[i].z);
            }
            _mesh.uv = uvs;
        }

        void Update()
        {
            GradientToTexture();
            
            float minTerrainHeight = _mesh.bounds.min.y + transform.position.y - 0.1f;
            float maxTerrainHeight = _mesh.bounds.max.y + transform.position.y + 0.1f;
        
            mat.SetTexture("terrainGradient", _gradientTexture);
        
            mat.SetFloat("minTerrainHeight", -3.1f);
            mat.SetFloat("maxTerrainHeight", 3.1f);
        }
        private void GradientToTexture()
        {
            _gradientTexture = new Texture2D(1, 100);
            Color[] pixelsColors = new Color[_gradientTexture.width * _gradientTexture.height];
     
            for (int i = 0; i < _gradientTexture.height; i++)
            {
                pixelsColors[i] = terrainGradient.Evaluate(i / (float)_gradientTexture.height);
            }
     
            _gradientTexture.SetPixels(pixelsColors);
            _gradientTexture.Apply();
        }

        private void ContiguousProceduralGrid()
        {
            _vertices = new Vector3[(gridSize + 1) * (gridSize + 1)];
            _triangles = new int[gridSize * gridSize * 6];
        
            float vertexOffset = cellSize * 0.5f;
            int v = 0;
            int t = 0;

            // Вершины
            for (int x = 0; x <= gridSize; x++)
            {
                for (int y = 0; y <= gridSize; y++)
                {
                    //float j = Mathf.PerlinNoise((float)x / gridSize, (float)y / gridSize)*20;
                    _vertices[v] = new Vector3(x * cellSize - vertexOffset, 0, y * cellSize - vertexOffset);
                    v++;
                }
            }
            
            // Треугольники
            v = 0;
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    // Четный квадрат
                    if ((x + y) % 2 == 0)
                    {
                        _triangles[t] = v;
                        _triangles[t + 1] = v + 1;
                        _triangles[t + 2] = v + gridSize + 1;
                        _triangles[t + 3] = v + gridSize + 1;
                        _triangles[t + 4] = v + 1;
                        _triangles[t + 5] = v + gridSize + 2;
                    }
                    // Нечётный квадрат
                    else
                    {
                        _triangles[t] = v + gridSize + 1;
                        _triangles[t + 1] = v;
                        _triangles[t + 2] = v + gridSize + 2;
                        _triangles[t + 3] = v + gridSize + 2;
                        _triangles[t + 4] = v;
                        _triangles[t + 5] = v + 1;
                    }

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
            
            _meshCollider.sharedMesh = _mesh;
        }
    
    }
}
