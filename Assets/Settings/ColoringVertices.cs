using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyTerrain
{
    public class ColoringVertices : MonoBehaviour
    {
        public Camera cam;
        public GameObject parentModel;
        public GameObject crossImageObject;
        public GameObject vertexSphere;
        public bool isSnapOn = true;

        private float sphereToScreenRatio = 25.0f;
        private int crossToScreenRatio = 25;
        private RaycastHit _raycastHit;
        private Image _crossImage;
        private GameObject _parentVertexSphere;

        private void Start()
        {
            vertexSphere.GetComponent<MeshRenderer>().enabled = false;
            _crossImage = crossImageObject.GetComponent<Image>();
            PoulateVertexSpheres();
            float sphereCastRadius = cam.orthographicSize / sphereToScreenRatio;
            ResizeVertexSphereCollider(sphereCastRadius);
        }

        void Update()
        {
            if (isSnapOn && !EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out _raycastHit))
                {
                    if (_raycastHit.transform.CompareTag("VertexSphere"))
                    {
                        GameObject sphereGameObject = _raycastHit.transform.gameObject;
                        crossImageObject.transform.position = cam.WorldToScreenPoint(sphereGameObject.transform.position);
                        _crossImage.rectTransform.sizeDelta = new Vector2(Screen.height / crossToScreenRatio,
                            Screen.height / crossToScreenRatio);
                        crossImageObject.SetActive(true);
                    }
                    else
                    {
                        crossImageObject.SetActive(false);
                    }
                }
            }
        }
        private void PoulateVertexSpheres()
        {
            if (parentModel != null)
            {
                if (_parentVertexSphere == null)
                {
                    _parentVertexSphere = new GameObject();
                }

                foreach (Transform child in parentModel.transform)
                {
                    Mesh mesh = child.GetComponent<MeshFilter>().mesh;
                    foreach (Vector3 vertex in mesh.vertices)
                    {
                        GameObject sphere = Instantiate(vertexSphere, child.transform.TransformPoint(vertex), Quaternion.identity,
                            _parentVertexSphere.transform);
                        sphere.tag = "VertexSphere";
                    }
                }
            }
        }

        private void ResizeVertexSphereCollider(float size)
        {
            if (parentModel != null)
            {
                foreach (Transform child in _parentVertexSphere.transform)
                {
                    child.GetComponent<SphereCollider>().radius = size;
                }
            }
        }
    }
}