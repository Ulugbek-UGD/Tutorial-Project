using UzGameDev.Helpers;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
    [AddComponentMenu("UzGameDev/Tiny Scripts/Camera/Simple Follow Camera 2D")]
    public class SimpleFollowCamera2D : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField, Range(1, 6)] private float damping = 5;
        [SerializeField] private Vector2 offset = new(0, 0);
        
        [Space(10)]
        [SerializeField] private Camera_Bounds_2D bounds;
        
        [Space(10)]
        [SerializeField] private Camera_Spline_2D spline;
        
        private Camera _camera;
        
        //============================================================
        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }
        //============================================================
        private void Start()
        {
            if (target.IsNull()) return;
            transform.position = DesiredPosition();
        }
        //============================================================
        private void LateUpdate()
        {
            if (target.IsNull()) return;
            transform.position = Vector3.Lerp(transform.position, DesiredPosition(), damping * Time.deltaTime);
        }
        //============================================================
        public void SetTarget(Transform value)
        {
            target = value;
        }
        //============================================================
        public void SetBound(Camera_Bounds_2D value)
        {
            bounds = value;
        }
        //============================================================
        public void SetSpline(Camera_Spline_2D value)
        {
            spline = value;
        }
        //============================================================
        private Vector3 DesiredPosition()
        {
            var targetPos = (Vector2)target.position + offset;
            if (spline.IsNotNull())
            {
                var closestPoint = GetClosestPathPoint(targetPos.x);
                var nextPoint = GetNextPathPoint(closestPoint, targetPos.x);
                var range = Mathf.InverseLerp(closestPoint.x, nextPoint.x, targetPos.x);
                var interpolatedY = Mathf.Lerp(closestPoint.y, nextPoint.y, range);
                targetPos.y = interpolatedY + offset.y;
            }
            if (bounds.IsNotNull())
            {
                var clamp = targetPos.Clamp(bounds.Min(_camera), bounds.Max(_camera));
                targetPos.x = clamp.x;
                targetPos.y = clamp.y;
            }
            return new Vector3(targetPos.x, targetPos.y, transform.position.z);
        }
        //============================================================
        private Vector2 GetClosestPathPoint(float targetX)
        {
            var closest = spline.points[0];
            var minDistance = Mathf.Abs(targetX - closest.x);
            foreach (var point in spline.points)
            {
                var distance = Mathf.Abs(targetX - point.x);
                if (distance < minDistance)
                {
                    closest = point;
                    minDistance = distance;
                }
            }
            return closest;
        }
        //============================================================
        private Vector2 GetNextPathPoint(Vector2 currentPoint, float targetX)
        {
            for (var i = 0; i < spline.points.Count; i++)
            {
                if (spline.points[i] == currentPoint)
                {
                    if (targetX > currentPoint.x && i + 1 < spline.points.Count)
                        return spline.points[i + 1];
                    if (targetX < currentPoint.x && i - 1 >= 0)
                        return spline.points[i - 1];
                }
            }
            return currentPoint;
        }
        //============================================================
    }
}