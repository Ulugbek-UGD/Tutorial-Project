using UnityEngine;

namespace UzGameDev.Helpers
{
    public static class ObjectHelpers
    {
        //============================================================
        /// <summary>
        /// Попытка получить указанный компонент у gameObject,
        /// если такой компонент не найден то добавляет новый и возвращает его.
        /// </summary>
        /// <param name="gameObject">gameObject.</param>
        /// <returns>Имеющий или добавленный компонент.</returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
        }
        //============================================================
        /// <summary>
        /// Проверяет, прикреплен ли к gameObject компонент типа T.
        /// </summary>
        /// <param name="gameObject">gameObject.</param>
        /// <returns>true если компонент имеется.</returns>
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>().IsNotNull();
        }
        //============================================================
        /// <summary>
        /// Проверка UnityEngine.Object на null (a != null)
        /// </summary>
        /// <param name="a">Object</param>
        /// <returns>bool result</returns>
        public static bool IsNotNull(this Object a)
        {
            return a;
        }
        //============================================================
        /// <summary>
        /// Проверка UnityEngine.Object на null (a == null)
        /// </summary>
        /// <param name="a">Object</param>
        /// <returns>bool result</returns>
        public static bool IsNull(this Object a)
        {
            return !a;
        }
        //============================================================
    }
}