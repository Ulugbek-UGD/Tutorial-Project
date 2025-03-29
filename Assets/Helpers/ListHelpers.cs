using System.Collections.Generic;
using UnityEngine;

namespace UzGameDev.Helpers
{
    public static class ListHelpers
    {
        //============================================================
        /// <summary>
        /// Добавить в список дочерних элементов по указанному типу
        /// </summary>
        /// <param name="root">(Transform) по которому будет произведен поиск</param>
        /// <param name="includeInactive">включить в поиск неактивных элементов?</param>
        /// <typeparam name="T">тип по которому будет произведен поиск</typeparam>
        /// <returns></returns>
        public static List<T> AddChildrenComponents<T>(this Transform root, bool includeInactive = false) where T: Component
        {
            var list = new List<T>();
            for (var index = 0; index < root.childCount; index++)
            {
                var child_GO = root.GetChild(index).gameObject;
                if (includeInactive)
                {
                    if (child_GO.TryGetComponent<T>(out var component))
                        list.Add(component);
                }
                else
                {
                    if (child_GO.activeSelf && child_GO.TryGetComponent<T>(out var component))
                        list.Add(component);
                }
            }
            return list;
        }
        //============================================================
        /// <summary>
        /// Добавить в список всех дочерних элементов по указанному типу
        /// </summary>
        /// <param name="root">(Transform) по которому будет произведен поиск</param>
        /// <param name="includeInactive">включить в поиск неактивных элементов?</param>
        /// <typeparam name="T">тип по которому будет произведен поиск</typeparam>
        /// <returns></returns>
        public static List<T> AddAllChildrenComponents<T>(this Transform root, bool includeInactive = false) where T: Component
        {
            var list = new List<T>();
            foreach (var component in root.GetComponentsInChildren<T>(includeInactive))
            {
                list.Add(component);
            }
            return list;
        }
        //============================================================
        /// <summary>
        /// Этот список нулевой или пустой?
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IList<T> list) => list == null || list.Count == 0;
        //============================================================
        /// <summary>
        /// Очистить список
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        public static void Clean<T>(this List<T> array) where T: Object
        {
            array.RemoveAll(item => item.IsNull());
        }
        //============================================================
        /// <summary>
        /// Очистить список
        /// </summary>
        /// <param name="rays"></param>
        /// <returns></returns>
        public static List<RaycastHit> Clean(this IEnumerable<RaycastHit> rays)
        {
            var list = new List<RaycastHit>();
            foreach (var ray in rays)
            {
                if (ray.collider.IsNull()) continue;
                list.Add(ray);
            }
            return list;
        }
        //============================================================
    }
}