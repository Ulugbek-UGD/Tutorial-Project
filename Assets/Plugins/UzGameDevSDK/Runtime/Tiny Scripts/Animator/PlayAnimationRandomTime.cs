using Random = UnityEngine.Random;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
	[AddComponentMenu("UzGameDev/Tiny Scripts/Animation/Play Animation Random Time")]
	[RequireComponent(typeof(Animator))]
	public class PlayAnimationRandomTime : MonoBehaviour
	{
		[SerializeField] private string stateName;
		[SerializeField] private int layerIndex;
		[SerializeField, Range(3, 30)] private int maxTime = 6;
		
		private Animator animator;
		
		//============================================================
		private void Awake()
		{
			animator = GetComponent<Animator>();
		}
		//============================================================
		private void OnEnable()
		{
			Invoke(nameof(Play), 1);
		}
		//============================================================
		private void OnDisable()
		{
			CancelInvoke(nameof(Play));
		}
		//============================================================
		private void Play()
		{
			animator.Play(stateName, layerIndex);
			Invoke(nameof(Play), Random.Range(0, maxTime));
		}
		//============================================================
	}
}