using UzGameDev.Helpers;
using UnityEngine;

namespace UzGameDev.TinyScripts
{
    [AddComponentMenu("UzGameDev/Tiny Scripts/UI/Sprite Health Bar")]
    public class SpriteHealthBar : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _fill;
        [SerializeField] private SpriteRenderer _background;
        
        [SerializeField] private bool _displayOnChange = true;
        [SerializeField, Range(1, 3)] private float _displayTime = 2;
        [SerializeField, Range(3, 6)] private float _fadeSpeed = 5;
        [SerializeField] private bool _lookToMainCamera;
        
        private float _fill_X_Scale;
        private float _currentAlpha;
        private float _targetAlpha;
        
        private Transform _cameraTransform;
        
        //============================================================
        private void Start()
        {
            _fill_X_Scale = _fill.transform.localScale.x;
            if (Camera.main != null) _cameraTransform = Camera.main.transform;
        }
        //============================================================
        public void DisplayHealth(float current, float max)
        {
            var fillTransform = _fill.transform;
            var scale = fillTransform.localScale;
            
            scale = new Vector3(_fill_X_Scale * current / max, scale.y, scale.z);
            fillTransform.localScale = scale;
            
            if (!_displayOnChange) return;
            _targetAlpha = 1;
            CancelInvoke(nameof(HideHealthBar));
            Invoke(nameof(HideHealthBar), _displayTime);
        }
        //============================================================
        private void HideHealthBar()
        {
            _targetAlpha = 0;
        }
        //============================================================
        private void Update()
        {
            if (_displayOnChange)
            {
                _currentAlpha = Mathf.MoveTowards(_currentAlpha, _targetAlpha, Time.deltaTime * _fadeSpeed);
            }
            else
            {
                _currentAlpha = 1;
            }
            _fill.SetAlpha(_currentAlpha);
            _background.SetAlpha(_currentAlpha);
        }
        //============================================================
        private void LateUpdate()
        {
            if (_lookToMainCamera) transform.LookAt(transform.position + _cameraTransform.forward);
        }
        //============================================================
    }
}