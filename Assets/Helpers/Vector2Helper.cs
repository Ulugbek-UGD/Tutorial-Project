using UnityEngine;

namespace UzGameDev.Helpers
{
	public static class Vector2Helpers
	{
		//============================================================
		/// <summary>
		/// Преобразование Vector2 в изометрический вид
		/// </summary>
		/// <param name="vector"></param>
		/// <param name="angle"></param>
		/// <returns></returns>
		public static Vector2 ConvertToIso(this Vector2 vector, Vector2 angle)
		{
			var vectorToV3 = new Vector3(vector.x, 0, vector.y);
			
			var yAxis = Quaternion.Euler(0, angle.y, 0);
			var isoMatrix = Matrix4x4.Rotate(yAxis);
			var result = isoMatrix.MultiplyPoint3x4(vectorToV3);
			
			var resultToV2 = new Vector2(result.x, result.z);
			return resultToV2;
		}
		//============================================================
		/// <summary>
		/// Фиксированное направление в 4 градусах
		/// </summary>
		/// <param name="direction"></param>
		/// <returns></returns>
		public static Vector2 Snapped4D(this Vector2 direction)
		{
			return direction.CalculateAngle(90f, 4) switch
			{
				0 => new Vector2(1f, 0f),
				1 => new Vector2(0f, 1f),
				2 => new Vector2(-1f, 0f),
				3 => new Vector2(0f, -1f),
				_ => Vector2.zero
			};
		}
		//============================================================
		/// <summary>
		/// Фиксированное направление в 8 градусах
		/// </summary>
		/// <param name="direction"></param>
		/// <returns></returns>
		public static Vector2 Snapped8D(this Vector2 direction)
		{
			return direction.CalculateAngle(45f, 8) switch
			{
				0 => new Vector2(1f, 0f),
				1 => new Vector2(0.71f, 0.71f),
				2 => new Vector2(0f, 1f),
				3 => new Vector2(-0.71f, 0.71f),
				4 => new Vector2(-1f, 0f),
				5 => new Vector2(-0.71f, -0.71f),
				6 => new Vector2(0f, -1f),
				7 => new Vector2(0.71f, -0.71f),
				_ => Vector2.zero
			};
		}
		//============================================================
		/// <summary>
		/// Фиксированное направление в 24 градусах
		/// </summary>
		/// <param name="direction"></param>
		/// <returns></returns>
		public static Vector2 Snapped24D(this Vector2 direction)
		{
			return direction.CalculateAngle(15f, 24) switch
			{
				0 => new Vector2(1f, 0f),
				1 => new Vector2(0.96f, 0.26f),
				2 => new Vector2(0.87f, 0.5f),
				3 => new Vector2(0.71f, 0.71f),
				4 => new Vector2(0.5f, 0.87f),
				5 => new Vector2(0.26f, 0.96f),
				6 => new Vector2(0f, 1f),
				7 => new Vector2(-0.26f, 0.96f),
				8 => new Vector2(-0.5f, 0.87f),
				9 => new Vector2(-0.71f, 0.71f),
				10 => new Vector2(-0.87f, 0.5f),
				11 => new Vector2(-0.96f, 0.26f),
				12 => new Vector2(-1f, 0f),
				13 => new Vector2(-0.96f, -0.26f),
				14 => new Vector2(-0.87f, -0.5f),
				15 => new Vector2(-0.71f, -0.71f),
				16 => new Vector2(-0.5f, -0.87f),
				17 => new Vector2(-0.26f, -0.96f),
				18 => new Vector2(0f, -1f),
				19 => new Vector2(0.26f, -0.96f),
				20 => new Vector2(0.5f, -0.87f),
				21 => new Vector2(0.71f, -0.71f),
				22 => new Vector2(0.87f, -0.5f),
				23 => new Vector2(0.96f, -0.26f),
				_ => Vector2.zero
			};
		}
		//============================================================
		/// <summary>
		/// Рассчитать угол
		/// </summary>
		/// <param name="direction"></param>
		/// <param name="degrees"></param>
		/// <param name="directions"></param>
		/// <returns></returns>
		private static int CalculateAngle(this Vector2 direction, float degrees, int directions)
		{
			if (direction == Vector2.zero)
			{
				return directions + 1;
			}
			
			var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			
			if (angle < 0f)
			{
				angle = 360f + angle;
			}
			
			angle += degrees / 2;
			var index = (int)(angle / degrees);
			index %= directions;
			
			return index;
		}
		//============================================================
		/// <summary>
		/// Ограничивает вектор
		/// </summary>
		/// <param name="value"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static Vector2 Clamp(this Vector2 value, Vector2 min, Vector2 max)
		{
			var x = Mathf.Clamp(value.x, min.x, max.x);
			var y = Mathf.Clamp(value.y, min.y, max.y);
			return new Vector2(x, y);
		}
		//============================================================
	}
}