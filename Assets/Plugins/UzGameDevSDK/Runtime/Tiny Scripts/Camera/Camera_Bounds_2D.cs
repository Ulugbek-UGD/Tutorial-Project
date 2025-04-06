using UnityEngine;

namespace UzGameDev.TinyScripts
{
    public class Camera_Bounds_2D : MonoBehaviour
    {
        public Vector2 bound = new(10, 10);
        public Vector2 offset;
        
        private Vector2 point => (Vector2)transform.position + offset;
        
        //============================================================
        public Vector2 Min(Camera cam)
        {
            var size = cam.orthographicSize;
            var halfWidth = cam.aspect * size;
            return new Vector2(point.x - bound.x / 2 + halfWidth, point.y - bound.y / 2 + size);
        }
        //============================================================
        public Vector2 Max(Camera cam)
        {
            var size = cam.orthographicSize;
            var halfWidth = cam.aspect * size;
            return new Vector2(point.x + bound.x / 2 - halfWidth, point.y + bound.y / 2 - size);
        }
        //============================================================
    }
}