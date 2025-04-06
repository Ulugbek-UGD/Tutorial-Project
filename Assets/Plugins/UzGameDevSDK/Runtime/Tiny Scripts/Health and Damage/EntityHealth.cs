using CustomInspector;
using UnityEngine;
using UltEvents;

namespace UzGameDev.TinyScripts
{
    [AddComponentMenu("UzGameDev/Tiny Scripts/Entity/Entity Health")]
    public class EntityHealth : MonoBehaviour
    {
        public bool Immortal;
        
        [SerializeField, Range(_min, _max), ShowIfNot(nameof(Immortal))]
        private float _maxHealth = 100;
        
        [SerializeField, ReadOnly, ShowIfNot(nameof(Immortal))]
        private float _currentHealth;
        
        [Space(10)]
        public UltEvent<float, float> OnHealthChange;
        public UltEvent<float> OnHealthFull;
        public UltEvent OnHealthOver;
        
        public float MaxHealth
        {
            get => _maxHealth;
            set
            {
                _maxHealth = value switch
                {
                    < _min => _min,
                    > _max => _max,
                    _ => value
                };
            }
        }
        public float CurrentHealth => _currentHealth;
        
        private const float _min = 1;
        private const float _max = 9999;
        
        //============================================================
        public void Start()
        {
            _currentHealth = _maxHealth;
        }
        //============================================================ +=
        public void IncreaseHealth(float value)
        {
            if (Immortal || value <= 0 || _currentHealth >= _maxHealth) return;
            
            _currentHealth += value;
            
            if (_currentHealth >= _maxHealth)
            {
                _currentHealth = _maxHealth;
                OnHealthFull?.Invoke(_currentHealth);
            }
            
            OnHealthChange?.Invoke(_currentHealth, _maxHealth);
        }
        //============================================================ -=
        public void ReduceHealth(float value)
        {
            if (Immortal || value <= 0 || _currentHealth <= 0) return;
            
            _currentHealth -= value;
            
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                OnHealthOver?.Invoke();
            }
            
            OnHealthChange?.Invoke(_currentHealth, _maxHealth);
        }
        //============================================================
    }
}