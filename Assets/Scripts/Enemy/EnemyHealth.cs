using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth;
        private int _currentHealth;

        public event Action<int, int> InitEvent;
        public event Action<int, int> OnDamagedEvent;
        public event Action OnDiedEvent;

        private void Awake() => _currentHealth = maxHealth;

        private void Start() => InitEvent?.Invoke(_currentHealth, maxHealth);

        public void TakeDamage(int amount)
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