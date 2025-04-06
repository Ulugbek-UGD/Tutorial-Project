using System.Collections.Generic;
using UnityEngine;

namespace UzGameDev.Helpers
{
    public static class GameObjectHelpers
    {
        //============================================================
        /// <summary>
        /// Работает точно так же как "gameObject.CompareTag()" но с коллекциями
        /// </summary>
        /// <param name="go">Target</param>
        /// <param name="tags">Tags Collection</param>
        /// <returns>bool result</returns>
        public static bool CompareTags(this GameObject go, IEnumerable<string> tags)
        {
            foreach (var tag in tags)
            {
                if (go.CompareTag(tag))
                {
                    return true;
                }
            }
            return false;
        }
        //============================================================
    }
}