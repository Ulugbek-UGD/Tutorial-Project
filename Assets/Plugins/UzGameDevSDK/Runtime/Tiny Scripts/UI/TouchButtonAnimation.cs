using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
	[AddComponentMenu("UzGameDev/Tiny Scripts/UI/Touch Button Animation")]
	[RequireComponent(typeof(Image))]
	public class TouchButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		public float downScale = 0.9f;
		public Color downColor = new(1f, 1f, 1f, 0.59f);
		public Color upColor = new(1f, 1f, 1f, 0.315f);
		
		private Image touchButton;
		
		//============================================================
		private void Awake()
		{
			touchButton = GetComponent<Image>();
		}
		//============================================================
		public void OnPointerDown(PointerEventData eventData)
		{
			touchButton.rectTransform.localScale = new Vector3(downScale,downScale,downScale);
			touchButton.color = downColor;
		}
		//============================================================
		public void OnPointerUp(PointerEventData eventData)
		{
			touchButton.rectTransform.localScale = new Vector3(1,1,1);
			touchButton.color = upColor;
		}
		//============================================================
	}
}