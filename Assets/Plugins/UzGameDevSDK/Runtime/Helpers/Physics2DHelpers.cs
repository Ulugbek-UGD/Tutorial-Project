using System.Collections.Generic;
using UnityEngine;

namespace UzGameDev.Helpers
{
    public static class Physics2DHelpers
    {
        //============================================================
        public static GameObject[] OverlapPointAll2D(this Vector2 point)
        {
            var colliders = Physics2D.OverlapPointAll(point);
            
            var targets = new List<GameObject>();
            
            if (colliders.Length <= 0) return targets.ToArray();
            
            foreach (var coll in colliders)
            {
                targets.Add(coll.gameObject);
            }
            return targets.ToArray();
        }
        //============================================================
        public static GameObject[] OverlapCircleAll2D(this Transform transform, Vector2 offset, float radius)
        {
            var scaleRadius = transform.ScaleToRadius(radius) * 0.98f;
            var colliders = Physics2D.OverlapCircleAll(transform.Centre(offset), scaleRadius);
            
            var targets = new List<GameObject>();
            
            if (colliders.Length <= 0) return targets.ToArray();
            
            foreach (var coll in colliders)
            {
                targets.Add(coll.gameObject);
            }
            return targets.ToArray();
        }
        //============================================================
        public static GameObject[] OverlapBoxAll2D(this Transform transform, Vector2 offset, Vector2 size)
        {
            var scaleSize = Vector3.Scale(size, transform.lossyScale) * 0.96f;
            var colliders = Physics2D.OverlapBoxAll(transform.Centre(offset), scaleSize, transform.eulerAngles.z);
            
            var targets = new List<GameObject>();
            
            if (colliders.Length <= 0) return targets.ToArray();
            
            foreach (var coll in colliders)
            {
                targets.Add(coll.gameObject);
            }
            return targets.ToArray();
        }
        //============================================================
    }
}