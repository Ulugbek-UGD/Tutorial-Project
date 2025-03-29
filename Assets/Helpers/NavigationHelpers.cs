using UnityEngine.AI;
using UnityEngine;

namespace UzGameDev.Helpers
{
    public static class NavigationHelpers
    {
        //===============================================================
        public static void VisualizePath(this NavMeshAgent agent, LineRenderer line, float width, Material material, Color color)
        {
            if (agent.hasPath && agent.remainingDistance > agent.stoppingDistance)
            {
                var path = agent.path;
                line.positionCount = path.corners.Length;
                
                for (var i = 0; i < path.corners.Length; i++)
                {
                    line.SetPosition(i, path.corners[i]);
                }
                
                line.startWidth = width;
                line.endWidth = width;
                
                line.numCapVertices = 3;
                
                line.material = material;
                
                line.startColor = color;
                line.endColor = color;
            }
            else
            {
                line.positionCount = 0;
            }
        }
        //===============================================================
    }
}