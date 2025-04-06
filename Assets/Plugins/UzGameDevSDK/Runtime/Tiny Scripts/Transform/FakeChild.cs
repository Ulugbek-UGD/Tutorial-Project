using UzGameDev.Helpers;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
#if UNITY_EDITOR
    [ExecuteAlways]
#endif
    [AddComponentMenu("UzGameDev/Tiny Scripts/Transform/Fake Child")]
    public class FakeChild : MonoBehaviour
    {
    	[SerializeField] private Transform fakeParent;
        
        private Vector3 startParentPosition;
        private Quaternion startParentRotation;
        private Vector3 startParentScale;
    	
        private Vector3 startPosition;
        private Quaternion startRotation;
        private Vector3 startScale;
    	
        private Matrix4x4 parentMatrix;
        
        private Transform currentParent;
        private bool initialized;
        
        //============================================================
    	private void Start ()
        {
	        InitializeParent();
        }
        //============================================================
        private void InitializeParent()
        {
	        if (fakeParent.IsNull() || initialized) return;
	        
	        currentParent = fakeParent;
	        
	        startParentPosition = fakeParent.position;
	        startParentRotation = fakeParent.rotation;
	        startParentScale = fakeParent.lossyScale;
	        
	        startPosition = transform.position;
	        startRotation = transform.rotation;
	        startScale = transform.lossyScale;
	        
	        startPosition = Vector3Helpers.DivideVectors(Quaternion.Inverse(fakeParent.rotation) * (startPosition - startParentPosition), startParentScale);
	        
	        initialized = true;
        }
        //============================================================
    	private void Update ()
    	{
    		if (fakeParent.IsNull()) return;
    		
            if (currentParent == fakeParent)
            {
	            parentMatrix = Matrix4x4.TRS(fakeParent.position, fakeParent.rotation, fakeParent.lossyScale);
	            transform.position = parentMatrix.MultiplyPoint3x4(startPosition);
	            transform.rotation = fakeParent.rotation * Quaternion.Inverse(startParentRotation) * startRotation;
	            transform.localScale = Vector3.Scale(startScale, Vector3Helpers.DivideVectors(fakeParent.lossyScale, startParentScale));
            }
            else
            {
	            initialized = false;
	            InitializeParent();
            }
        }
        //============================================================
    }
}