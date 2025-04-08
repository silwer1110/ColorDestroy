using UnityEngine;

public class RayCaster : MonoBehaviour
{ 
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.TryGetComponent(out Cube cube))
                {
                    cube.Destroy();
                }
            }
        }
    }
}