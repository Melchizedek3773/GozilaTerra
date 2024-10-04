using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CreateTerrain : MonoBehaviour
{
    [SerializeField] int m;
    [SerializeField] int p;
    
    [SerializeField] int iOffset;
    [SerializeField] int kOffset;

    [SerializeField] float noiseScale = 0.03f;
    [SerializeField] float jMultiplier = 7;
    
    [SerializeField] Gradient terrainGradient;
    [SerializeField] Material mat;
    
    private Mesh _mesh;
    private MeshCollider _meshCollider;
    private Texture2D _gradientTexture;
    private void Start()
    {
        _meshCollider = GetComponent<MeshCollider>();
        
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        _mesh.name = "Terrain";
        
        CreateTerrainMesh();
        GradientToTexture();
        
        _meshCollider.sharedMesh = _mesh;
    }
    private void Update()
    {
        CreateTerrainMesh();
        GradientToTexture();
        
        float minTerrainHeight = _mesh.bounds.min.y + transform.position.y - 0.1f;
        float maxTerrainHeight = _mesh.bounds.max.y + transform.position.y + 0.1f;
        
        mat.SetTexture("terrainGradient", _gradientTexture);
        
        mat.SetFloat("minTerrainHeight", minTerrainHeight);
        mat.SetFloat("maxTerrainHeight", maxTerrainHeight);
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
    private void CreateTerrainMesh()
    {
        // Vertices
        Vector3[] rectangleA = new Vector3[(m + 1) * (p + 1)];
        for (int a = 0, k = 0; k <= p; k++)
        {
            for (int i = 0; i <= m; i++)
            {
                float j = Mathf.PerlinNoise((i + iOffset) * noiseScale, (k + kOffset) * noiseScale) * jMultiplier;
                
                rectangleA[a] = new Vector3(i, j, k);
                a++;
            }
        }
        // Triangles
        int[] triangles = new int[m * p * 6];
        
        int vertex = 0;
        int triangleIndex = 0;
        for (int k = 0; k < p; k++)
        {
            for (int i = 0; i < m; i++)
            {
                triangles[triangleIndex + 0] = vertex + 0;
                triangles[triangleIndex + 1] = vertex + m + 1;
                triangles[triangleIndex + 2] = vertex + 1;
                
                triangles[triangleIndex + 3] = vertex + 1;
                triangles[triangleIndex + 4] = vertex + m + 1;
                triangles[triangleIndex + 5] = vertex + m + 2;
                
                vertex++;
                triangleIndex += 6;
            }
            vertex++;
        }
        _mesh.Clear();
        
        _mesh.SetVertices(rectangleA);
        _mesh.SetTriangles(triangles, 0);
        _mesh.RecalculateNormals();
    }
}