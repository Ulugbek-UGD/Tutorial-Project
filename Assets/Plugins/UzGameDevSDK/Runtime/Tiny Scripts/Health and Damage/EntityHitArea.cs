using System.Collections.Generic;
using MiniTools.BetterGizmos;
using UzGameDev.Helpers;
using CustomInspector;
using UnityEngine;
using UltEvents;

namespace UzGameDev.TinyScripts
{
    [AddComponentMenu("UzGameDev/Tiny Scripts/Entity/Entity Hit Area")]
    public class EntityHitArea : MonoBehaviour
    {
        private enum HitType { TwoD, ThreeD }
        private enum HitShape2D { Square, Circle }
        private enum HitShape3D { Box, Sphere }
        
        [SerializeField] private HitType _hitType = HitType.TwoD;
        //------------------------------------------------------------ 2D
        [SerializeField, ShowIf(nameof(_hit2D))]
        private HitShape2D _hitShape2D = HitShape2D.Square;
        
        [SerializeField, Min(-10), Max(10), ShowIf(nameof(_hit2D))]
        private Vector2 _offset2D;
        
        [SerializeField, Min(0.1f), Max(20), ShowIf(BoolOperator.And, nameof(_hit2D), nameof(_square))]
        private Vector2 _size2D = Vector2.one;
        //------------------------------------------------------------ 3D
        [SerializeField, ShowIf(nameof(_hit3D))]
        private HitShape3D _hitShape3D = HitShape3D.Box;
        
        [SerializeField, Min(-10), Max(10), ShowIf(nameof(_hit3D))]
        private Vector3 _offset3D;
        
        [SerializeField, Min(0.1f), Max(20), ShowIf(BoolOperator.And, nameof(_hit3D), nameof(_square))]
        private Vector3 _size3D = Vector3.one;
        
        //------------------------------------------------------------ Common
        [SerializeField, Range(0.05f, 10), ShowIf(nameof(_circle))]
        private float _radius = 0.5f;
        
        [SerializeField, Range(1, 10), ShowIf(nameof(_hit3D))]
        private int _maxColliderHit = 1;
        
        [Space(10)]
        [SerializeField, Range(_min, _max)] private float _damage = 1;
        
        [Space(10)]
        public UltEvent<float, GameObject> OnHit;
        
        public float Damage
        {
            get => _damage;
            set
            {
                _damage = value switch
                {
                    < _min => _min,
                    > _max => _max,
                    _ => value
                };
            }
        }
        
        private const float _min = 1;
        private const float _max = 9999;
        
        private bool _hit2D;
        private bool _hit3D;
        private bool _square;
        private bool _circle;
        
#if UNITY_EDITOR
        [Button(nameof(DoHit))]
        [HideField] public string button;
#endif
        //============================================================
        public void DoHit()
        {
            var targets = System.Array.Empty<GameObject>();
            switch (_hitType)
            {
                case HitType.TwoD:
                    switch (_hitShape2D)
                    {
                        case HitShape2D.Square:
                            targets = transform.OverlapBoxAll2D(_offset2D, _size2D);
                            break;
                        case HitShape2D.Circle:
                            targets = transform.OverlapCircleAll2D(_offset2D, _radius);
                            break;
                    }
                    break;
                case HitType.ThreeD:
                    switch (_hitShape3D)
                    {
                        case HitShape3D.Box:
                            targets = transform.OverlapBox(_offset3D, _size3D, _maxColliderHit);
                            break;
                        case HitShape3D.Sphere:
                            targets = transform.OverlapSphere(_offset3D, _radius, _maxColliderHit);
                            break;
                    }
                    break;
            }
            DamageHurts(targets);
        }
        //============================================================
        private void DamageHurts(IReadOnlyCollection<GameObject> objects)
        {
            if (objects.Count <= 0) return;
            foreach (var obj in objects)
            {
                if (obj.TryGetComponent<EntityHurtArea>(out var hurtArea))
                {
                    hurtArea.ApplyDamage(_damage, gameObject);
                    OnHit?.Invoke(_damage, gameObject);
                }
            }
        }
        //============================================================
#if UNITY_EDITOR
        private void Reset()
        {
            UpdateSettings();
        }
        //============================================================
        private void OnValidate()
        {
            UpdateSettings();
        }
        //============================================================
        private void UpdateSettings()
        {
            _hit2D = _hitType == HitType.TwoD;
            _hit3D = _hitType != HitType.TwoD;
            if (_hit2D)
            {
                _square = _hitShape2D == HitShape2D.Square;
                _circle = _hitShape2D != HitShape2D.Square;
            }
            else
            {
                _square = _hitShape3D == HitShape3D.Box;
                _circle = _hitShape3D != HitShape3D.Box;
            }
        }
        //============================================================
        public void OnDrawGizmosSelected()
        {
            var color = Color.red;
            var position = transform.position;
            var origin2D = (Vector2)position + _offset2D;
            var origin3D = position + _offset3D;
            
            switch (_hitType)
            {
                case HitType.TwoD:
                    switch (_hitShape2D)
                    {
                        case HitShape2D.Square:
                            BetterGizmos.DrawBox2D(color, origin2D, _size2D, transform.eulerAngles.z);
                            break;
                        case HitShape2D.Circle:
                            BetterGizmos.DrawCircle2D(color, origin2D, Vector3.back, _radius);
                            break;
                    }
                    break;
                case HitType.ThreeD:
                    switch (_hitShape3D)
                    {
                        case HitShape3D.Box:
                            BetterGizmos.DrawBox(color, origin3D, transform.rotation, _size3D);
                            break;
                        case HitShape3D.Sphere:
                            BetterGizmos.DrawSphere(color, origin3D, _radius);
                            break;
                    }
                    break;
            }
        }
        //============================================================
#endif
    }
}