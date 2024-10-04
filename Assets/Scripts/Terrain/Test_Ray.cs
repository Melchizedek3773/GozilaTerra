using UnityEngine;

public class Test_Ray : MonoBehaviour
{
    public Camera cam;
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hitInfo))
        {
            Debug.Log(hitInfo.point + " Pressed primary button. ");
        }
    }
}