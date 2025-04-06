using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine;

namespace UzGameDev.Helpers
{
    public static class ColorHelpers
    {
        //============================================================
        /// <summary>
        /// Change Graphic color alpha value
        /// </summary>
        public static void SetAlpha(this Graphic graphic, float value)
        {
            graphic.color = graphic.color.SetColorElement(3, value);
        }
        //============================================================
        /// <summary>
        /// Change SpriteRenderer color alpha value
        /// </summary>
        public static void SetAlpha(this SpriteRenderer sprite, float value)
        {
            sprite.color = sprite.color.SetColorElement(3, value);
        }
        //============================================================
        /// <summary>
        /// Change Tilemap color alpha value
        /// </summary>
        public static void SetAlpha(this Tilemap tilemap, float value)
        {
            tilemap.color = tilemap.color.SetColorElement(3, value);
        }
        //============================================================
        /// <summary>
        /// Change Color element value
        /// </summary>
        private static Color SetColorElement(this Color color, int index, float value)
        {
            color[index] = value;
            return color;
        }
        //============================================================
    }
}