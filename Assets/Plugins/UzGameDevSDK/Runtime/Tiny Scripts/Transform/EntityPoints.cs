using System.Collections.Generic;
using UzGameDev.Helpers;
using UnityEngine;
using System;

namespace UzGameDev.TinyScripts
{
    public class EntityPoints : MonoBehaviour
    {
        public List<Vector3> Points { get; private set;}
        
        //============================================================
        [Serializable]
        public class Point
        {
            public Vector3 value;
            public Vector3 offset;
        }
        [SerializeField] private Point[] points = Array.Empty<Point>();
        //============================================================
        private void Start()
        {
            Points = new List<Vector3>();
            foreach (var point in points)
            {
                Points.Add(point.value);
            }
        }
        //============================================================
        private void Update()
        {
            for (var i = 0; i < points.Length; i++)
            {
                points[i].value = Vector3Helpers.MovePointAroundPivot(transform.position + points[i].offset, transform.position, transform.rotation);
                Points[i] = points[i].value;
            }
        }
        //============================================================
        
        
#if UNITY_EDITOR
        //============================================================
        private void OnValidate()
        {
            if (points.Length <= 0) return;
            foreach (var point in points)
            {
                point.offset = point.offset.x switch
                {
                    < -10 => new Vector3(-10, 0, point.offset.z),
                    > 10 => new Vector3(10, 0, point.offset.z),
                    _ => point.offset
                };
                point.offset = point.offset.y switch
                {
                    < -10 => new Vector3(point.offset.x, -10, point.offset.z),
                    > 10 => new Vector3(point.offset.x, 10, point.offset.z),
                    _ => point.offset
                };
                point.offset = point.offset.z switch
                {
                    < -10 => new Vector3(point.offset.x, 0, -10),
                    > 10 => new Vector3(point.offset.x, 0, 10),
                    _ => point.offset
                };
                point.value = Vector3Helpers.MovePointAroundPivot(transform.position + point.offset, transform.position, transform.rotation);
            }
            
            Points = new List<Vector3>();
            foreach (var point in points)
            {
                Points.Add(point.value);
            }
        }
        //============================================================
        private void OnDrawGizmos()
        {
            if (points.Length <= 0) return;
            foreach (var point in points)
            {
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.color = new Color(0, 1, 1, 0.5f);
                Gizmos.DrawWireSphere(Vector3.zero + point.offset, 0.1f);
            }
        }
        //============================================================
#endif
    }
}