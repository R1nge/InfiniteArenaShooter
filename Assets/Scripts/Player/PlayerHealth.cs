using System;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        private PlayerStats _playerStats;
        private float _currentHealth;

        public event Action<float, float> InitEvent;
        public event Action<float, float> OnDamagedEvent;
        public event Action OnDiedEvent;

        [Inject]
        private void Construct(PlayerStats playerStats)
        {
            _playerStats = playerStats;
        }

        private void Awake()
        {
            _currentHealth = _playerStats.GetMaxHealth();
            OnDiedEvent += OnOnDiedEvent;
        }

        private void Start() => InitEvent?.Invoke(_currentHealth, _playerStats.GetMaxHealth());

        public void TakeDamage(float amount)
        {
            _currentHealth -= amount;

            if (_currentHealth <= 0)
            {
                OnDiedEvent?.Invoke();
            }
            else
            {
                OnDamagedEvent?.Invoke(_currentHealth, _playerStats.GetMaxHealth());
            }
        }

        private void OnOnDiedEvent()
        {
            //TODO: show game over UI
        }

        private void OnDestroy() => OnDiedEvent -= OnOnDiedEvent;
    }
}