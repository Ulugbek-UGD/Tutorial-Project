using UzGameDev.Helpers;
using UnityEngine;
using UltEvents;

namespace UzGameDev.TinyScripts
{
    [AddComponentMenu("UzGameDev/Tiny Scripts/Entity/Entity Hurt Area")]
    public class EntityHurtArea : MonoBehaviour
    {
        public bool IgnoreHit;
        
        [SerializeField, Range(_min, _max)]
        private float _damageMultiplier = 1;
        
        [Space(10)]
        public UltEvent<float, GameObject> OnHurt;
        
        public float DamageMultiplier
        {
            get => _damageMultiplier;
            set
            {
                _damageMultiplier = value switch
                {
                    < _min => _min,
                    > _max => _max,
                    _ => value
                };
            }
        }
        
        private const float _min = 0.1f;
        private const float _max = 10;
        
        //============================================================
        public void ApplyDamage(float damage, GameObject dealer)
        {
            if (IgnoreHit || damage <= 0 || dealer.IsNull()) return;
            OnHurt?.Invoke(damage * _damageMultiplier, dealer);
        }
        //============================================================
    }
}