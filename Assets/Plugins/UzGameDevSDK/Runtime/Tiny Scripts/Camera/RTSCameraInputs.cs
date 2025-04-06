using UnityEngine.InputSystem;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
    public class RTSCameraInputs : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActions;
        
        private InputActionMap actionMap;
        
        public static InputAction AxisKey { get; private set; }
        public static InputAction E { get; private set; }
        public static InputAction Q { get; private set; }
        
        public static InputAction RightMouseButton { get; private set; }
        public static InputAction MousePosition { get; private set; }
        public static InputAction MouseScroll { get; private set; }
        
        //============================================================
        private void Awake()
        {
            FindInputActions();
        }
        //============================================================
        private void OnEnable()
        {
            inputActions.Enable();
        }
        //============================================================
        private void OnDisable()
        {
            inputActions.Disable();
        }
        //============================================================
        private void FindInputActions()
        {
            actionMap = inputActions.FindActionMap("Main");
            
            AxisKey = actionMap.FindAction("Axis Key");
            E = actionMap.FindAction("E Key");
            Q = actionMap.FindAction("Q Key");
            
            RightMouseButton = actionMap.FindAction("Right Mouse Button");
            MousePosition = actionMap.FindAction("Mouse Position");
            MouseScroll = actionMap.FindAction("Mouse Scroll");
        }
        //============================================================
    }
}