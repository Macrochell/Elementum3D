using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    public LayerMask groundLayer; 

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
         
            offset = transform.position - hit.point;
        }
    }

    private void OnMouseDrag()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            
            transform.position = hit.point + offset;
        }
    }
}
