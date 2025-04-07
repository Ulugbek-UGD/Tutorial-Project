using ClickerGame;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LastStandHeroes
{
    public class UnitSelector : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputActions;
        
        private InputActionMap _inputMap;
        
        private static InputAction Click;
        private static InputAction PointerPosition;
        
        private static Vector2 _pointerPosition => PointerPosition.ReadValue<Vector2>();
        
        private Camera _camera;
        
        //===============================================================
        private void Awake()
        {
            _inputMap = _inputActions.FindActionMap("Main");
            
            Click = _inputMap.FindAction("Click");
            PointerPosition = _inputMap.FindAction("PointerPosition");
            
            if (Camera.main != null)
            {
                _camera = Camera.main;
            }
        }
        //===============================================================
        private void OnEnable()
        {
            _inputActions.Enable();
            Click.performed += OnClick;
        }
        //===============================================================
        private void OnDisable()
        {
            _inputActions.Disable();
            Click.performed -= OnClick;
        }
        //===============================================================
        private void OnClick(InputAction.CallbackContext context)
        {
            var rayOrigin = _camera.ScreenPointToRay(_pointerPosition);
            
            if (Physics.Raycast(rayOrigin, out var hitInfo, _camera.farClipPlane))
            {
                if (hitInfo.collider.TryGetComponent<ISelectable>(out var selectable))
                {
                    selectable.Select();
                }
            }
        }
        //===============================================================
    }
}