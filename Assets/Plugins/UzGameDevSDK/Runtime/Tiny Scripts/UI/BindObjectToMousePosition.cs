using UnityEngine.InputSystem;
using UzGameDev.Helpers;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
	[AddComponentMenu("UzGameDev/Tiny Scripts/UI/Bind Object To Mouse Position")]
	public class BindObjectToMousePosition : MonoBehaviour
	{
		private Camera mainCamera;
		
		//============================================================
		private void Awake()
		{
			if (Camera.main.IsNotNull()) mainCamera = Camera.main;
		}
		//============================================================
		private void LateUpdate()
		{
			var mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.value);
			transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
		}
		//============================================================
	}
}