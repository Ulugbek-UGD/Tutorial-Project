using UzGameDev.Helpers;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
    [AddComponentMenu("UzGameDev/Tiny Scripts/VFX/Draw Line")]
    public class DrawLine : MonoBehaviour
    {
        [SerializeField] private Transform lineStart;
        [SerializeField] private Transform lineEnd;
        [SerializeField] private float lineWidth = 0.1f;
        [SerializeField] private Material customMaterial;
        
        private LineRenderer lineRenderer;
        
        //============================================================
        private void Start()
        {
            if (lineStart.IsNull() || lineEnd.IsNull()) return;
            
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.useWorldSpace = true;
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.material = customMaterial == null ? new Material(Shader.Find("UI/Unlit/Text")) : customMaterial;
        }
        //============================================================
        private void Update()
        {
            if (lineStart.IsNull() || lineEnd.IsNull()) return;
            
            var positions = new Vector3[2];
            positions[0] = lineStart.position;
            positions[1] = lineEnd.position;
            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
        }
        //============================================================
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (lineStart.IsNull() || lineEnd.IsNull()) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(lineStart.position, 0.2f);
            Gizmos.DrawWireSphere(lineEnd.position, 0.2f);
            Gizmos.DrawLine(lineStart.position, lineEnd.position);
        }
        //============================================================
#endif
    }
}