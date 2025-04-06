using System.Collections.Generic;
using UzGameDev.Helpers;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
    public class NearestFinder : MonoBehaviour
    {
        [SerializeField] private bool drawGizmosLine;
        
        public Transform Target;
        public Vector3 Direction;
        public float Distance;
        
        [Space(10)]
        public List<Transform> targets;
        
        //============================================================
        private void FixedUpdate()
        {
            if (targets.Count <= 0) return;
            
            Target = transform.NearestTarget(targets);
            
            if (Target.IsNotNull())
            {
                Direction = (Target.position - transform.position).normalized;
                Distance = Vector3.Distance(transform.position, Target.position);
            }
            else
            {
                Direction = Vector3.zero;
                Distance = 0;
            }
        }
        //============================================================
#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            if (!drawGizmosLine || Target.IsNull()) return;
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawLine(transform.position, Target.position);
        }
        //============================================================
#endif
    }
}