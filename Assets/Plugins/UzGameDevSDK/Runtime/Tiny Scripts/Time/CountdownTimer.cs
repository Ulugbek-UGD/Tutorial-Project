using UnityEngine.Events;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
	[AddComponentMenu("UzGameDev/Tiny Scripts/Time/Countdown Timer")]
	public class CountdownTimer : MonoBehaviour
    {
	    [SerializeField, Min(1)] private float time;
	    [SerializeField] private bool runOnStart;
        [SerializeField] private bool loop;
        
        public UnityEvent OnTimeout;
        public UnityEvent<float> OnTimeUpdate;
        
        private bool counting;
	    private float currentTime;
	    
	    //============================================================
	    private void Start()
        {
            if (runOnStart)
            {
                StartTimer();
            }
        }
	    //============================================================
	    private void Update()
        {
	        if(!counting) return;
	        switch (currentTime)
	        {
		        case > 0:
			        currentTime -= Time.deltaTime;
			        OnTimeUpdate?.Invoke(currentTime);
			        break;
		        case < 0:
		        {
			        StopTimer();
			        OnTimeout?.Invoke();
			        if (loop) StartTimer();
			        break;
		        }
	        }
        }
	    //============================================================
        public void StartTimer()
	    {
		    counting = true;
            currentTime = time;
	    }
        //============================================================
	    public void StopTimer()
	    {
	    	counting = false;
	    	currentTime = 0;
	    }
	    //============================================================
	    public void PauseTimer()
	    {
	    	counting = false;
	    }
	    //============================================================
	    public void ResumeTimer()
	    {
	    	counting = true;
	    }
	    //============================================================
	    public void ResetTimer()
	    {
		    counting = false;
		    currentTime = time;
	    }
	    //============================================================
    }
}