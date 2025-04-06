using System.Collections.Generic;
using UnityEngine;
using UzGameDev.Helpers;

namespace LastStandHeroes
{
    public class NewMonoBehaviourScript : MonoBehaviour
    {
        public List<Transform> targets;
        
        //===============================================================
        private void Update()
        {
            var nearestTarget = transform.NearestTarget(targets);
            Debug.DrawLine(transform.position, nearestTarget.position, Color.red);
        }
        //===============================================================
    }
}