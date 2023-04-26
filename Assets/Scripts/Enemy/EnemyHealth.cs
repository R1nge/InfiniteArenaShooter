using System;
using PlayerWeapons;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private EnemyType type;
        private float _currentHealth;

        public event Action<float, float> InitEvent;
        public event Action<float, float> OnDamagedEvent;
        public event Action OnDiedEvent;

        private void Awake() => _currentHealth = maxHealth;

        private void Start() => InitEvent?.Invoke(_currentHealth, maxHealth);

        public void TakeDamage(float amount, EnemyType enemyType)
        {
            if (type != enemyType) return;
            
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