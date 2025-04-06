using System.Collections.Generic;
using UzGameDev.Helpers;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
    public class Camera_Spline_2D : MonoBehaviour
    {
        public List<Vector2> points;
        
#if UNITY_EDITOR
        //============================================================
        private void OnDrawGizmosSelected()
        {
            if (points.IsNullOrEmpty() || points.Count < 2) return;
            
            Gizmos.color = Color.yellow;
            
            var lastPoint = points[0];
            for (var index = 1; index < points.Count; index++)
            {
                Gizmos.DrawLine(points[index], lastPoint);
                lastPoint = points[index];
            }
        }
        //============================================================
#endif
    }
}