using UnityEngine;

namespace UzGameDev.TinyScripts
{
	[AddComponentMenu("UzGameDev/Tiny Scripts/VFX/Sprite Fader")]
	public class SpriteFader : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer[] sprites;
		
		[Header("Fade Options")]
		[SerializeField, Range(0, 1)] private float fadeIn = 1;
		[SerializeField, Range(0, 1)] private float fadeOut = 0.5f;
		[SerializeField, Range(1, 4)] private float fadeSpeed = 2;
		
		private float currentAlpha = 1;
		private float targetAlpha = 1;
		
		//============================================================
		public void FadeIn()
		{
			targetAlpha = fadeIn;
		}
		//============================================================
		public void FadeOut()
		{
			targetAlpha = fadeOut;
		}
		//============================================================
		private void Update ()
		{
			currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, Time.deltaTime * fadeSpeed);
			
			foreach (var sprite in sprites)
			{
				var color = sprite.color;
				color.a = currentAlpha;
				sprite.color = color;
			}
		}
		//============================================================
	}
}