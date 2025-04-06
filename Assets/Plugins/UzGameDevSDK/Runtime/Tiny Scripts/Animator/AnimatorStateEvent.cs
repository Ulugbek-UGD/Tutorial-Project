using UnityEngine.Events;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
    [AddComponentMenu("UzGameDev/Tiny Scripts/Animation/Animator State Event")]
    [RequireComponent(typeof(Animator))]
    public class AnimatorStateEvent : MonoBehaviour
    {
        public UnityEvent<string> OnStateStart;
        public UnityEvent<string> OnStateComplete;
        
        private Animator animator;
        
        //============================================================
        private void Awake()
        {
            animator = GetComponent<Animator>();
            
            foreach (var clip in animator.runtimeAnimatorController.animationClips)
            {
                var animationStartEvent = new AnimationEvent
                    { time = 0, functionName = nameof(AnimationStartHandler), stringParameter = clip.name };
                var animationEndEvent = new AnimationEvent
                    { time = clip.length, functionName = nameof(AnimationCompleteHandler), stringParameter = clip.name };
                
                clip.AddEvent(animationStartEvent);
                clip.AddEvent(animationEndEvent);
            }
        }
        //============================================================
        private void AnimationStartHandler(string animName)
        {
            OnStateStart?.Invoke(animName);
        }
        //============================================================
        private void AnimationCompleteHandler(string animName)
        {
            OnStateComplete?.Invoke(animName);
        }
        //============================================================
    }
}