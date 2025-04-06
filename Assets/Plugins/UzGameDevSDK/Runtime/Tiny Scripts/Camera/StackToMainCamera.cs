using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
    public class StackToMainCamera : MonoBehaviour
    {
        private Camera mainCamera;
        private Camera thisCamera;
        
        private UniversalAdditionalCameraData cameraData;
        
        //============================================================
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        //============================================================
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        //============================================================
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (Camera.main != null) mainCamera = Camera.main;
            cameraData = mainCamera.GetComponent<UniversalAdditionalCameraData>();
            thisCamera = GetComponent<Camera>();
            
            if (cameraData.cameraStack.Contains(thisCamera)) return;
            cameraData.cameraStack.Add(thisCamera);
        }
        //============================================================
    }
}