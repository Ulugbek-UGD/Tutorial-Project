using System.Collections.Generic;
using UnityEngine;

namespace UzGameDev.Helpers
{
    public static class TransformHelpers
    {
        //============================================================
        /// <summary>
        /// Возвращает самую дальнюю цель от себя, из списка целей.
        /// </summary>
        /// <param name="origin">Transform Self</param>
        /// <param name="targets">Targets List</param>
        /// <param name="after">Farthest After Distance</param>
        /// <returns>Transform closestTarget</returns>
        public static Transform FarthestTarget(this Transform origin, List<Transform> targets, float after = 0)
        {
            if (targets.Count <= 0) return null;
            //--------------------------
            targets.RemoveAll(item => item.IsNull());
            //--------------------------
            if (targets.Count <= 0) return null;
            //--------------------------
            Transform farthestTarget = null;
            var distance = Mathf.NegativeInfinity;
            foreach (var target in targets)
            {
                var farthestDistance = Vector3.Distance(origin.position, target.position);
                if (farthestDistance > distance)
                {
                    if (farthestDistance > after) continue;
                    farthestTarget = target;
                    distance = farthestDistance;
                }
            }
            return farthestTarget;
        }
        //============================================================
        /// <summary>
        /// Возвращает самую ближнюю цель от себя, из списка целей.
        /// </summary>
        /// <param name="origin">Transform Self</param>
        /// <param name="targets">Targets List</param>
        /// <param name="after">Nearest After Distance</param>
        /// <returns>Transform closestTarget</returns>
        public static Transform NearestTarget(this Transform origin, List<Transform> targets, float after = 0)
        {
            if (targets.Count <= 0) return null;
            //--------------------------
            targets.RemoveAll(item => item.IsNull());
            //--------------------------
            if (targets.Count <= 0) return null;
            //--------------------------
            Transform nearestTarget = null;
            var distance = Mathf.Infinity;
            foreach (var target in targets)
            {
                var nearestDistance = Vector3.Distance(origin.position, target.position);
                if (nearestDistance < distance)
                {
                    if (nearestDistance < after) continue;
                    nearestTarget = target;
                    distance = nearestDistance;
                }
            }
            return nearestTarget;
        }
        //============================================================
        /// <summary>
        /// Сбрасывает позицию, поворот и масштаб
        /// </summary>
        /// <param name="transform">Self</param>
        /// <param name="local">Rest to Local</param>
        public static void ResetTransform(this Transform transform, bool local = false)
        {
            if (local)
            {
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
            }
            else
            {
                transform.position = Vector3.zero;
                transform.rotation = Quaternion.identity;
            }
            transform.localScale = Vector3.one;
        }
        //============================================================
        public static float ScaleToRadius(this Transform transform, float radius)
        {
            var lossyScale = transform.lossyScale;
            var x = Mathf.Abs(lossyScale.x);
            var y = Mathf.Abs(lossyScale.y);
            var z = Mathf.Abs(lossyScale.z);
            var absoluteRadius = Mathf.Max(Mathf.Max(x, y), z) * radius;
            return absoluteRadius;
        }
        //============================================================
        public static void SetPosition(this GameObject go, float x, float y, float z)
        {
            go.transform.position = new Vector3(x, y, z);
        }
        //============================================================
        public static void SetPositionX(this GameObject go, float value)
        {
            go.transform.position = new Vector3(value, go.transform.position.y, go.transform.position.z);
        }
        //============================================================
        public static void SetPositionY(this GameObject go, float value)
        {
            go.transform.position = new Vector3(go.transform.position.x, value, go.transform.position.z);
        }
        //============================================================
        public static void SetPositionZ(this GameObject go, float value)
        {
            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, value);
        }
        //============================================================
        public static void SetEulerAngles(this GameObject go, float x, float y, float z)
        {
            go.transform.eulerAngles = new Vector3(x, y, z);
        }
        //============================================================
        public static void SetEulerAnglesX(this GameObject go, float value)
        {
            go.transform.eulerAngles = new Vector3(value, go.transform.eulerAngles.y, go.transform.eulerAngles.z);
        }
        //============================================================
        public static void SetEulerAnglesY(this GameObject go, float value)
        {
            go.transform.eulerAngles = new Vector3(go.transform.eulerAngles.x, value, go.transform.eulerAngles.z);
        }
        //============================================================
        public static void SetEulerAnglesZ(this GameObject go, float value)
        {
            go.transform.eulerAngles = new Vector3(go.transform.eulerAngles.x, go.transform.eulerAngles.y, value);
        }
        //============================================================
        public static void SetScale(this GameObject go, float x, float y, float z)
        {
            go.transform.localScale = new Vector3(x, y, z);
        }
        //============================================================
    }
}