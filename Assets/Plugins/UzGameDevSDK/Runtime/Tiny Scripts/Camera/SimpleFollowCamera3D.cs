using UzGameDev.Helpers;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
    [AddComponentMenu("UzGameDev/Tiny Scripts/Camera/Simple Follow Camera 3D")]
    public class SimpleFollowCamera3D : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField, Range(1, 6)] private float speed = 3;
        [SerializeField] private Vector3 offset = new(0, 1, 0);
        
        //============================================================
        private void Start()
        {
            if (target.IsNull()) return;
            transform.position = target.position + offset;
        }
        //============================================================
        private void LateUpdate()
        {
            if (target.IsNull()) return;
            
            var interpolation = speed * Time.deltaTime;
            
            var cameraPosition = transform.position;
            var targetPosition = target.position;
            
            var smoothX = Mathf.Lerp(cameraPosition.x, targetPosition.x + offset.x, interpolation);
            var smoothY = Mathf.Lerp(cameraPosition.y, targetPosition.y + offset.y, interpolation);
            var smoothZ = Mathf.Lerp(cameraPosition.z, targetPosition.z + offset.z, interpolation);
            
            transform.position = new Vector3(smoothX, smoothY, smoothZ);
        }
        //============================================================
        public void SetTarget(Transform value)
        {
            target = value;
        }
        //============================================================
    }
}