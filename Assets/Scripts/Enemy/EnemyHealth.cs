using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth;
        private float _currentHealth;

        public event Action<float, float> InitEvent;
        public event Action<float, float> OnDamagedEvent;
        public event Action OnDiedEvent;

        private void Awake() => _currentHealth = maxHealth;

        private void Start() => InitEvent?.Invoke(_currentHealth, maxHealth);

        public void TakeDamage(float amount)
        {
            _currentHealth -= amount;

            if (_currentHealth <= 0)
            {
                OnDiedEvent?.Invoke();
            }
            else
            {
                OnDamagedEvent?.Invoke(_currentHealth, maxHealth);
            }
        }
    }
}