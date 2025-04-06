#if UNITY_EDITOR
using UnityEngine;

namespace UzGameDev
{
    [ExecuteInEditMode]
    public class SnapObjectInEditMode : MonoBehaviour
    {
        [SerializeField] private float size = 25.6f;
        [SerializeField] private Vector3 offset;
        
        //============================================================
        private void Update()
        {
            if (Application.isPlaying) return;
            var currentPosition = transform.position;
            var snappedX = Mathf.Round(currentPosition.x / size) * size + offset.x;
            var snappedY = offset.y;
            var snappedZ = Mathf.Round(currentPosition.z / size) * size + offset.z;
            var snappedPosition = new Vector3(snappedX, snappedY, snappedZ);
            transform.position = snappedPosition;
        }
        //============================================================
    }
}
#endif