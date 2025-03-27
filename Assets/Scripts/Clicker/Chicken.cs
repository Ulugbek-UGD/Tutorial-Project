using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chicken : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private LayerMask layerMask;
    
    // 1
    private void OnMouseDown()
    {
        Debug.Log(gameObject.name + " clicked");
    }
    
    private void Update()
    {
        // 2
        if (Input.GetMouseButton(0))
        {
            // 3D Raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100, layerMask) && hit.collider != null)
            {
                Debug.Log(gameObject.name + " clicked");
            }
            
            // 2D Raycast
            var worldPosition = Camera.main.WorldToScreenPoint(Input.mousePosition);
            if (Physics2D.Raycast(worldPosition, Camera.main.transform.forward, layerMask))
            {
                Debug.Log(gameObject.name + " clicked");
            }
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(gameObject.name + " clicked");
    }
}