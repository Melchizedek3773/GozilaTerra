using UnityEngine;

namespace Terrain
{
    public class TerrainTerraforming : MonoBehaviour
    {
        [SerializeField] Camera cam; 
        [SerializeField] float terraformingEffeciency = 0.01f;
        [SerializeField] float terraformingRadius = 3;
    
        private MeshFilter _meshFilter;
        private MeshCollider _meshCollider;
        void Start()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshCollider = GetComponent<MeshCollider>();
        }
        void Update()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if ( Input.GetMouseButton(0) && Physics.Raycast(ray, out hitInfo) )
            {
                TerraformTerrain(hitInfo.point, terraformingEffeciency, terraformingRadius);
                //Debug.Log(hitInfo.point + " Pressed primary button. ");
            }
            if ( Input.GetMouseButton(1) && Physics.Raycast(ray, out hitInfo) )
            {
                TerraformTerrain(hitInfo.point, -terraformingEffeciency, terraformingRadius);
                //Debug.Log("Pressed secondary button.");
            }
        } 
    
        private Mesh _mesh;
        private Vector3[] _vertices;
        private void TerraformTerrain(Vector3 pos, float height, float range)
        {
            _mesh = _meshFilter.sharedMesh;
            _vertices = _mesh.vertices;
            pos -= _meshFilter.transform.position;

            int a = 0;
            foreach (Vector3 vert in _vertices)
            {
                if (Vector2.Distance( new Vector2(vert.x, vert.z), new Vector2(pos.x, pos.z)) <= range )
                {
                    _vertices[a] = vert + new Vector3(0, height, 0);
                }
                a++;
            }
        
            _mesh.SetVertices(_vertices);
            _meshFilter.mesh = _mesh;
            _meshCollider.sharedMesh = _mesh;
        }
    }
}
