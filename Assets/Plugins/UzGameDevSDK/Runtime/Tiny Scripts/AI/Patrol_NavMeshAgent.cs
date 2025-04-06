using CustomInspector;
using UnityEngine.AI;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
    public class Patrol_NavMeshAgent : MonoBehaviour
    {
        [SerializeField, Range(0, 10)] private float waitTime = 2;
        
        [Space]
        [SerializeField] private Transform[] patrolPoints;
        
        private NavMeshAgent agent;
        
        [Space]
        [SerializeField, ReadOnly] private float timer;
        [SerializeField, ReadOnly] private int currentPointIndex;
        
        //============================================================
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }
        //============================================================
        private void Update()
        {
            if (agent.pathPending || agent.remainingDistance > agent.stoppingDistance) return;
            
            if (timer <= 0)
            {
                MoveToNextPoint();
                timer = waitTime;
            }
            else timer -= Time.deltaTime;
        }
        //============================================================
        private void MoveToNextPoint()
        {
            if (patrolPoints.Length == 0) return;
            agent.SetDestination(patrolPoints[currentPointIndex].position);
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
        //============================================================
    }
}