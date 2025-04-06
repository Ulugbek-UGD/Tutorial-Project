using UnityEngine.InputSystem;
using UnityEngine;
using System;

namespace UzGameDev.Input
{
    [AddComponentMenu("UzGameDev/Input/Input Devise Status Monitor")]
    public sealed class InputDeviseStatusMonitor : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputAction;
        [SerializeField] private bool hideCursorOnGamepad;
        [SerializeField] private bool enableLogs;
        
        public static event Action<InputDevice> OnDeviseAdded;
        public static event Action<InputDevice> OnDeviseRemoved;
        public static event Action<InputDevice> OnDeviseUsed;
        
        public static InputDevice CurrentDevice { get; private set; }
        
        private InputActionMap inputActionMap;
        private InputAction anyKeyButton;
        private InputAction anyMouseButton;
        private InputAction anyGamepadButton;
        
        //============================================================
        private void Awake()
        {
            inputActionMap = inputAction.FindActionMap("Main");
            anyKeyButton = inputActionMap.FindAction("AnyKeyButton");
            anyMouseButton = inputActionMap.FindAction("AnyMouseButton");
            anyGamepadButton = inputActionMap.FindAction("AnyGamepadButton");
        }
        //============================================================
        private void OnEnable()
        {
            inputActionMap.Enable();
            InputSystem.onDeviceChange += CheckChangedDevice;
            anyKeyButton.performed += CheckUsedDevice;
            anyMouseButton.performed += CheckUsedDevice;
            anyGamepadButton.performed += CheckUsedDevice;
        }
        //============================================================
        private void OnDisable()
        {
            inputActionMap.Disable();
            InputSystem.onDeviceChange -= CheckChangedDevice;
            anyKeyButton.performed -= CheckUsedDevice;
            anyMouseButton.performed -= CheckUsedDevice;
            anyGamepadButton.performed -= CheckUsedDevice;
        }
        //============================================================
        private void CheckChangedDevice(InputDevice device, InputDeviceChange change)
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                {
                    OnDeviseAdded?.Invoke(device);
                    if (enableLogs) Debug.Log($"Device: <color=#4DFF4D>{device.displayName}</color> added:");
                    break;
                }
                case InputDeviceChange.Removed:
                {
                    OnDeviseRemoved?.Invoke(device);
                    if (enableLogs) Debug.Log($"Device: <color=#FF4D4D>{device.displayName}</color> removed:");
                    break;
                }
            }
        }
        //============================================================
        private void CheckUsedDevice(InputAction.CallbackContext context)
        {
            CurrentDevice = context.control.device;
            OnDeviseUsed?.Invoke(CurrentDevice);
            
            if (hideCursorOnGamepad && CurrentDevice == Gamepad.current)
            {
                Cursor.visible = false;
            }
            else
            {
                Cursor.visible = true;
            }
            
            if (enableLogs) Debug.Log($"Device: <color=#00C8FF>{CurrentDevice.displayName}</color> used");
        }
        //============================================================
    }
}