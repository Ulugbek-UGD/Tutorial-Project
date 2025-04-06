using UzGameDev.Helpers;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
	[AddComponentMenu("UzGameDev/Tiny Scripts/Transform/LookAt 2D")]
	public class LookAt2D : MonoBehaviour
	{
		[SerializeField] private Transform target;
		[SerializeField] private float speed = 10f;
		[SerializeField] private float offset;
		
		//============================================================
		private void LateUpdate()
		{
			if (target.IsNull()) return;
			
			var direction = target.position - transform.position;
			var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			var rotation = Quaternion.AngleAxis(angle + offset, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
		}
		//============================================================
	}
}