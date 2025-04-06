using System.Collections.Generic;
using UzGameDev.Helpers;
using UnityEngine;

namespace UzGameDev
{
    public static class PhysicsHelpers
    {
        //============================================================
        public static GameObject[] OverlapSphere(this Transform transform, Vector3 offset, float radius, int maxColliderHit)
        {
            var colliders = new Collider[maxColliderHit];
            
            var scaleRadius = transform.ScaleToRadius(radius);
            Physics.OverlapSphereNonAlloc(transform.Centre(offset), scaleRadius, colliders);
            
            var targets = new List<GameObject>();
            
            if (colliders.Length <= 0) return targets.ToArray();
            
            foreach (var coll in colliders)
            {
                if (coll.IsNull()) continue;
                targets.Add(coll.gameObject);
            }
            return targets.ToArray();
        }
        //============================================================
        public static GameObject[] OverlapBox(this Transform transform, Vector3 offset, Vector3 size, int maxColliderHit)
        {
            var colliders = new Collider[maxColliderHit];
            
            var scaleSize = Vector3.Scale(size, transform.lossyScale) * 0.5f;
            Physics.OverlapBoxNonAlloc(transform.Centre(offset), scaleSize, colliders, transform.rotation);
            
            var targets = new List<GameObject>();
            
            if (colliders.Length <= 0) return targets.ToArray();
            
            foreach (var coll in colliders)
            {
                if (coll.IsNull()) continue;
                targets.Add(coll.gameObject);
            }
            return targets.ToArray();
        }
        //============================================================
    }
}