using UnityEngine;

namespace UzGameDev.Helpers
{
    public static class FloatHelpers
    {
        //============================================================
        /// <summary>
        /// Дистанция между двумя векторами 2D.
        /// </summary>
        /// <param name="a">Self</param>
        /// <param name="b">Target</param>
        /// <returns>float distance</returns>
        public static float Distance2D(Vector2 a, Vector2 b)
        {
            return (a - b).sqrMagnitude;
        }
        //============================================================
        /// <summary>
        /// Дистанция между двумя векторами 3D.
        /// </summary>
        /// <param name="a">Self</param>
        /// <param name="b">Target</param>
        /// <returns>float distance</returns>
        public static float Distance3D(Vector3 a, Vector3 b)
        {
            return (a - b).sqrMagnitude;
        }
        //============================================================
    }
}