using UnityEngine;

namespace UzGameDev.Helpers
{
	public static class BoolHelpers
	{
		//============================================================
		/// <summary>
		/// Находится ли цель спереди или сзади?
		/// </summary>
		/// <param name="transform">Owner</param>
		/// <param name="target">Target</param>
		/// <returns></returns>
		public static bool IsInFront(this Transform transform, Transform target)
		{
			var direction = target.transform.position - transform.position;
			return Vector3.Dot(transform.forward, direction) > 0.0f;
		}
		//============================================================
		/// <summary>
		/// Находится ли цель сверху или снизу?
		/// </summary>
		/// <param name="transform">Owner</param>
		/// <param name="target">Target</param>
		/// <returns></returns>
		public static bool IsAbove(this Transform transform, Transform target)
		{
			var direction = target.transform.position - transform.position;
			return Vector3.Dot(transform.up, direction) > 0.0f;
		}
		//============================================================
		/// <summary>
		/// Находится ли цель справа или слева?
		/// </summary>
		/// <param name="transform">Owner</param>
		/// <param name="target">Target</param>
		/// <returns></returns>
		public static bool IsAtTheRight(this Transform transform, Transform target)
		{
			var direction = target.transform.position - transform.position;
			return Vector3.Dot(transform.right, direction) > 0.0f;
		}
		//============================================================
	}
}