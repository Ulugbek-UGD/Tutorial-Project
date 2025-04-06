using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

namespace UzGameDev.Helpers
{
    public static class UiHelpers
    {
        //============================================================
        /// <summary>
        /// Находится ли "указатель" над объектом пользовательского интерфейса?
        /// </summary>
        /// <param name="pointer">позиция указателя (мышь / палец / ручка )</param>
        /// <returns>bool – result</returns>
        public static bool PointedOnUiObject(this Vector2 pointer)
        {
            var eventDataCurrentPosition = new PointerEventData(EventSystem.current)
            {
                position = pointer
            };
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
        //============================================================
        /// <summary>
        /// Определяет, полностью ли виден этот RectTransform с указанной камеры
        /// Работает путем проверки того, находится ли каждый угол ограничивающей рамки этого RectTransform внутри усеченного изображения пространства экрана камеры.
        /// </summary>
        /// <param name="rectTransform">RectTransform</param>
        /// <param name="camera">Camera</param>
        /// <returns>true – если полностью виден с указанной камеры; в противном случае – false</returns>
        public static bool IsFullyVisibleFrom(this RectTransform rectTransform, Camera camera)
        {
            return CountCornersVisibleFrom(rectTransform, camera) == 4; // true, если видны все 4 угла
        }
        //============================================================
        /// <summary>
        /// Определяет, виден ли этот RectTransform хотя бы частично с указанной камеры.
        /// Работает, проверяя, находится ли какой-либо угол ограничивающей рамки этого RectTransform внутри усеченного изображения пространства экрана камеры.
        /// </summary>
        /// <param name="rectTransform">RectTransform</param>
        /// <param name="camera">Camera</param>
        /// <returns>ture – если хотя бы частично виден с указанной камеры; в противном случае – false</returns>
        public static bool IsVisibleFrom(this RectTransform rectTransform, Camera camera)
        {
            return CountCornersVisibleFrom(rectTransform, camera) > 0; // true если видны углы.
        }
        //============================================================
        /// <summary>
        /// Подсчитывает углы ограничивающей рамки данного RectTransform, видимые с данной камеры в пространстве экрана.
        /// </summary>
        /// <param name="rectTransform">RectTransform</param>
        /// <param name="camera">Camera</param>
        /// <returns>Количество углов ограничивающей рамки, видимых с камеры</returns>
        private static int CountCornersVisibleFrom(this RectTransform rectTransform, Camera camera)
        {
            var screenBounds = new Rect(0f, 0f, Screen.width, Screen.height); // Границы экранного пространства (предполагается, что камера рендерит по всему экрану)
            var objectCorners = new Vector3[4];
            rectTransform.GetWorldCorners(objectCorners);
            
            var visibleCorners = 0;
            foreach (var corner in objectCorners)
            {
                var tempScreenSpaceCorner = camera.WorldToScreenPoint(corner); // Кэшировано.
                if (screenBounds.Contains(tempScreenSpaceCorner)) // Если угол находится внутри экрана.
                {
                    visibleCorners++;
                }
            }
            return visibleCorners;
        }
        //============================================================
        /// <summary>
        /// Мировое позиция элемента пользовательского интерфейса
        /// </summary>
        /// <param name="rt">RectTransform</param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static Vector2 WorldPositionOfUiElement(this RectTransform rt, Camera camera)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, rt.position, camera, out var result);
            return result;
        }
        //============================================================
        /// <summary>
        /// Локальная позиция элемента пользовательского интерфейса
        /// </summary>
        /// <param name="rt">RectTransform</param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static Vector2 LocalPositionOfUiElement(this RectTransform rt, Camera camera)
        {
            var rectScreen = new Vector2(rt.localPosition.x + (float)Screen.width / 2, rt.localPosition.y + (float)Screen.height / 2);
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, rectScreen, camera, out var result);
            return result;
        }
        //============================================================
        public static Vector3 WorldPositionFor(this Canvas canvas, Vector3 point)
        {
            var screenPoint = canvas.worldCamera.WorldToScreenPoint(point);
            screenPoint.z = (canvas.transform.position - canvas.worldCamera.transform.position).magnitude;
            return canvas.worldCamera.ScreenToWorldPoint(screenPoint);
        }
        //============================================================
        public static Vector3 WorldPosition(this RectTransform rt, Camera camera)
        {
            var pos = camera.ViewportToWorldPoint(rt.position);
            return camera.WorldToViewportPoint(pos);
        }
        //============================================================
    }
}