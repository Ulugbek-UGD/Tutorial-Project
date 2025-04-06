using UzGameDev.Helpers;
using CustomInspector;
using UnityEngine.AI;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
    public class Path_Visualizer_NavMeshAgent : MonoBehaviour
    {
        [SerializeField, Range(0.02f, 0.1f)] private float updateRate = 0.05f;
        
        [Space]
        [SerializeField, Range(0.05f, 0.3f)] private float lineWidth = 0.1f;
        
        [Space]
        [SerializeField] private Material lineMaterial;
        [SerializeField] private Color lineColor = new(1, 0.64f, 0, 1);
        
        private NavMeshAgent agent;
        private LineRenderer line;
        
        [Space]
        [SerializeField, ReadOnly] private float timer;
        
        //============================================================
        private void Awake()
        {
            line = gameObject.AddComponent<LineRenderer>();
            agent = gameObject.GetComponent<NavMeshAgent>();
        }
        //============================================================
        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                agent.VisualizePath(line, lineWidth, lineMaterial, lineColor);
                timer = updateRate;
            }
        }
        //============================================================
    }
}