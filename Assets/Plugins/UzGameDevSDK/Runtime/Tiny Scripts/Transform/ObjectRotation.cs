using UnityEngine;

namespace UzGameDev.TinyScripts
{
	[AddComponentMenu("UzGameDev/Tiny Scripts/Transform/Object Rotation")]
	public class ObjectRotation : MonoBehaviour
	{
		private enum RotateIn
		{ 
			FixedUpdate,
			Update,
			LateUpdate
		}
		[Header("Update mode")]
		[SerializeField] private RotateIn rotateIn = RotateIn.Update;
		
		[Header("Direction of rotation")]
		[SerializeField, Range(-1, 1)] private int x;
		[SerializeField, Range(-1, 1)] private int y;
		[SerializeField, Range(-1, 1)] private int z = 1;
		
		[Header("Rotational frequency")]
		[SerializeField, Range(0, 1800)] private float speed = 180;
		
		//============================================================
		private void FixedUpdate ()
		{
			if (rotateIn != RotateIn.FixedUpdate) return;
			transform.Rotate(new Vector3(x, y, z) * (speed * Time.deltaTime));
		}
		//============================================================
		private void Update ()
		{
			if (rotateIn != RotateIn.Update) return;
			transform.Rotate(new Vector3(x, y, z) * (speed * Time.deltaTime));
		}
		//============================================================
		private void LateUpdate ()
		{
			if (rotateIn != RotateIn.LateUpdate) return;
			transform.Rotate(new Vector3(x, y, z) * (speed * Time.deltaTime));
		}
		//============================================================
	}
}