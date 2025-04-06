using UzGameDev.Helpers;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
    [RequireComponent(typeof(RTSCameraInputs))]
    public class RTSCamera : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField, Range(10, 80)] private float _moveSpeed = 40;
        private const float _screenEdgeThreshold = 4;
        private float _moveSmoothTime;
        private const float _baseMoveSmoothTime = 0.1f;
        private const float _panMoveSmoothTime = 0.04f;
        private Vector3 _targetPosition;
        private Vector3 _dragStartPoint;
        private Vector3 _moveVelocity;
        
        [Header("Rotation")]
        [SerializeField, Range(45, 360)] private float _rotationSpeed = 180;
        private const float _rotationSmoothTime = 0.1f;
        private float _targetYRotation;
        private float _currentYRotation;
        private float _yRotationVelocity;
        
        [Header("Zoom")]
        [SerializeField, Range(0.5f, 2)] private float _zoomSpeed = 1;
        [SerializeField, Range(1, 5)] private float _minZoom = 2;
        [SerializeField, Range(6, 12)] private float _maxZoom = 8;
        private const float _zoomSmoothTime = 0.1f;
        private float _targetZoom;
        private float _currentZoom;
        private float _zoomVelocity;
        
        private Camera _camera;
        
        private static Vector2 AxisKey => RTSCameraInputs.AxisKey.ReadValue<Vector2>();
        private static bool E_KeyHold => RTSCameraInputs.E.IsPressed();
        private static bool Q_KeyHold => RTSCameraInputs.Q.IsPressed();
        private static bool Right_Mouse_Button_Down => RTSCameraInputs.RightMouseButton.WasPressedThisFrame();
        private static bool Right_Mouse_Button_Hold => RTSCameraInputs.RightMouseButton.IsPressed();
        private static Vector2 MousePosition => RTSCameraInputs.MousePosition.ReadValue<Vector2>();
        private static float MouseScroll => RTSCameraInputs.MouseScroll.ReadValue<float>();
        
        //===============================================================
        private void Awake()
        {
            _camera = GetComponentInChildren<Camera>();
        }
        //===============================================================
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Initialize_Movement();
            Initialize_Rotation();
            Initialize_Zoom();
        }
        //===============================================================
        private void Update()
        {
            Update_Movement();
            Update_Rotation();
            Update_Zoom();
        }
        //===============================================================
        private void Initialize_Movement()
        {
            _targetPosition = new Vector3(transform.position.x, 0, transform.position.z);
        }
        private void Update_Movement()
        {
            Keyboard_Axis();
            Screen_Edge_Scroll();
            Mouse_Panning();
            
            var damp = Vector3.SmoothDamp(transform.position, _targetPosition, ref _moveVelocity, _moveSmoothTime);
            transform.position = new Vector3(damp.x, 0, damp.z);
        }
        private void Keyboard_Axis()
        {
            if (AxisKey != Vector2.zero)
            {
                var isoKeyAxis = new Vector3(AxisKey.x, 0, AxisKey.y).ConvertToIso(transform.eulerAngles.y);
                var zoomFactor = _currentZoom / _maxZoom;
                _targetPosition += isoKeyAxis * (_moveSpeed * zoomFactor * Time.deltaTime);
            }
        }
        private void Screen_Edge_Scroll()
        {
            var screenDirection = Vector3.zero;
            if (MousePosition != Vector2.zero)
            {
                if (MousePosition.x >= Screen.width - _screenEdgeThreshold && MousePosition.x <= Screen.width)
                    screenDirection.x += 1;
                if (MousePosition.x is <= _screenEdgeThreshold and > -1)
                    screenDirection.x -= 1;
                if (MousePosition.y >= Screen.height - _screenEdgeThreshold && MousePosition.y <= Screen.height)
                    screenDirection.z += 1;
                if (MousePosition.y is <= _screenEdgeThreshold and > -1)
                    screenDirection.z -= 1;
            }
            if (screenDirection != Vector3.zero)
            {
                screenDirection = screenDirection.ConvertToIso(transform.eulerAngles.y);
                var zoomFactor = _currentZoom / _maxZoom;
                _targetPosition += screenDirection.normalized * (_moveSpeed * zoomFactor * Time.deltaTime);
            }
        }
        private void Mouse_Panning()
        {
            var plane = new Plane(Vector3.up, Vector3.zero);
            var ray = _camera.ScreenPointToRay(MousePosition);
            
            if (plane.Raycast(ray, out var entry))
            {
                if (Right_Mouse_Button_Down)
                {
                    _dragStartPoint = ray.GetPoint(entry);
                }
                if (Right_Mouse_Button_Hold)
                {
                    var dragCurrentPoint = ray.GetPoint(entry);
                    _targetPosition = transform.position + _dragStartPoint - dragCurrentPoint;
                    _moveSmoothTime = _panMoveSmoothTime;
                }
                else
                {
                    _moveSmoothTime = _baseMoveSmoothTime;
                }
            }
        }
        //===============================================================
        private void Initialize_Rotation()
        {
            _targetYRotation = transform.eulerAngles.y;
            _currentYRotation = _targetYRotation;
        }
        private void Update_Rotation()
        {
            if (Q_KeyHold)
            {
                _targetYRotation += _rotationSpeed * Time.deltaTime;
            }
            if (E_KeyHold)
            {
                _targetYRotation -= _rotationSpeed * Time.deltaTime;
            }
            _currentYRotation = Mathf.SmoothDampAngle(_currentYRotation, _targetYRotation, ref _yRotationVelocity, _rotationSmoothTime);
            transform.rotation = Quaternion.Euler(45, _currentYRotation, 0);
        }
        //===============================================================
        private void Initialize_Zoom()
        {
            _targetZoom = _camera.orthographicSize;
            _currentZoom = _targetZoom;
        }
        private void Update_Zoom()
        {
            if (MouseScroll != 0)
            {
                _targetZoom -= MouseScroll * _zoomSpeed;
                _targetZoom = Mathf.Clamp(_targetZoom, _minZoom, _maxZoom);
            }
            _currentZoom = Mathf.SmoothDamp(_currentZoom, _targetZoom, ref _zoomVelocity, _zoomSmoothTime);
            _camera.orthographicSize = _currentZoom;
        }
        //===============================================================
#if UNITY_EDITOR
        private void Reset()
        {
            gameObject.name = "Isometric Camera Rig";
            
            var thisTransform = transform;
            var position = thisTransform.position;
            
            thisTransform.position = new Vector3(position.x, 0, position.z);
            thisTransform.eulerAngles = new Vector3(45, 45, 0);
            thisTransform.localScale = Vector3.one;
            
            _camera = GetComponentInChildren<Camera>();
            if (_camera == null) _camera = new GameObject("Camera").AddComponent<Camera>();
            
            var cameraTransform = _camera.transform;
            cameraTransform.SetParent(transform);
            cameraTransform.localPosition = new Vector3(0, 0, -13);
            cameraTransform.localRotation = Quaternion.identity;
            
            _camera.orthographic = true;
            _camera.orthographicSize = 5;
            _camera.nearClipPlane = 0;
            _camera.farClipPlane = 100;
            _camera.depth = -1;
            
            var cameraData = _camera.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>();
            if (cameraData != null) cameraData.renderPostProcessing = false;
        }
        //===============================================================
#endif
    }
}