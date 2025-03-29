using System.Collections.Generic;
using UnityEngine;

namespace UzGameDev.Helpers
{
    public static class Vector3Helpers
    {
        //============================================================
        /// <summary>
        /// Возвращает самую дальнюю точку от себя, из списка точек.
        /// </summary>
        /// <param name="origin">Transform Self</param>
        /// <param name="points">Targets List</param>
        /// <param name="after">Farthest After Distance</param>
        /// <returns>Transform closestTarget</returns>
        public static Vector3 FarthestPoint(this Transform origin, List<Vector3> points, float after = 0)
        {
            //--------------------------
            if (points.Count <= 0) return Vector3.zero;
            //--------------------------
            var farthestPoint = Vector3.zero;
            var distance = Mathf.NegativeInfinity;
            foreach (var point in points)
            {
                var farthestDistance = Vector3.Distance(origin.position, point);
                if (farthestDistance > distance)
                {
                    if (farthestDistance > after) continue;
                    farthestPoint = point;
                    distance = farthestDistance;
                }
            }
            return farthestPoint;
        }
        //============================================================
        /// <summary>
        /// Возвращает самую ближнюю точку от себя, из списка точек.
        /// </summary>
        /// <param name="origin">Transform Self</param>
        /// <param name="points">Targets List</param>
        /// <param name="after">Nearest After Distance</param>
        /// <returns>Transform closestTarget</returns>
        public static Vector3 NearestPoint(this Transform origin, List<Vector3> points, float after = 0)
        {
            //--------------------------
            if (points.Count <= 0) return Vector3.zero;
            //--------------------------
            var nearestPoint = Vector3.zero;
            var distance = Mathf.Infinity;
            foreach (var point in points)
            {
                var nearestDistance = Vector3.Distance(origin.position, point);
                if (nearestDistance < distance)
                {
                    if (nearestDistance < after) continue;
                    nearestPoint = point;
                    distance = nearestDistance;
                }
            }
            return nearestPoint;
        }
        //============================================================
        /// <summary>
        /// Преобразование Vector3 в изометрический вид
        /// </summary>
        /// <param name="vector">input axis</param>
        /// <param name="y">y angle</param>
        /// <returns>Isometric Axis</returns>
        public static Vector3 ConvertToIso(this Vector3 vector, float y)
        {
            var yAxis = Quaternion.Euler(0, y, 0);
            var isoMatrix = Matrix4x4.Rotate(yAxis);
            var result = isoMatrix.MultiplyPoint3x4(vector);
            return result;
        }
        //============================================================
        /// <summary>
        /// Переместить точку вокруг опорной точки
        /// </summary>
        /// <param name="point"></param>
        /// <param name="pivot"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static Vector3 MovePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
        {
            var direction = point - pivot;
            direction = rotation * direction;
            point = direction + pivot;
            return point;
        }
        //============================================================
        /// <summary>
        /// Деление вектора
        /// </summary>
        /// <param name="num"></param>
        /// <param name="den"></param>
        /// <returns></returns>
        public static Vector3 DivideVectors(Vector3 num, Vector3 den)
        {
            return new Vector3(num.x / den.x, num.y / den.y, num.z / den.z);
        }
        //============================================================
        /// <summary>
        /// Округляет вектор
        /// </summary>
        /// <param name="point"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Vector3 Round(this Vector3 point, float size = 1)
        {
            return new Vector3(
                Mathf.Round(point.x / size) * size,
                Mathf.Round(point.y / size) * size,
                Mathf.Round(point.z / size) * size);
        }
        //============================================================
        /// <summary>
        /// Округляет вектор со смещением
        /// </summary>
        /// <param name="point"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Vector3 RoundOffset(this Vector3 point, Vector3 offset, float size = 1)
        {
            var snapped = point + offset;
            snapped = new Vector3(
                Mathf.Round(snapped.x / size) * size,
                Mathf.Round(snapped.y / size) * size,
                Mathf.Round(snapped.z / size) * size);
            return snapped - offset;
        }
        //============================================================
        /// <summary>
        /// Ограничивает вектор
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vector3 Clamp(this Vector3 value, Vector3 min, Vector3 max)
        {
            var x = Mathf.Clamp(value.x, min.x, max.x);
            var y = Mathf.Clamp(value.y, min.y, max.y);
            var z = Mathf.Clamp(value.z, min.z, max.z);
            return new Vector3(x, y, z);
        }
        //============================================================
        /// <summary>
        /// Случайная точка внутри двумерного круга X / Y
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="minRadius"></param>
        /// <param name="maxRadius"></param>
        /// <returns></returns>
        public static Vector3 RandomPointInCircleXY(this Vector3 origin, float minRadius = 0, float maxRadius = 1)
        {
            var centre = new Vector2(origin.x, origin.y);
            var point = centre + Random.insideUnitCircle.normalized * Random.Range(minRadius, maxRadius);
            return new Vector3(point.x, point.y, origin.z);
        }
        //============================================================
        /// <summary>
        /// Случайная точка внутри двумерного круга X / Z
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="minRadius"></param>
        /// <param name="maxRadius"></param>
        /// <returns></returns>
        public static Vector3 RandomPointInCircleXZ(this Vector3 origin, float minRadius = 0, float maxRadius = 1)
        {
            var centre = new Vector2(origin.x, origin.z);
            var point = centre + Random.insideUnitCircle.normalized * Random.Range(minRadius, maxRadius);
            return new Vector3(point.x, origin.y, point.y);
        }
        //============================================================
        /// <summary>
        /// Переместить точку вокруг опорной точки с учетом масштаба и смешения
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static Vector3 Centre(this Transform transform, Vector3 offset)
        {
            var position = transform.position;
            var scaleOffset = Vector3.Scale(offset, transform.lossyScale);
            return MovePointAroundPivot(position + scaleOffset, position, transform.rotation);
        }
        //============================================================
    }
}