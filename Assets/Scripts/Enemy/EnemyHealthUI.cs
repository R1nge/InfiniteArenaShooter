using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class EnemyHealthUI : MonoBehaviour
    {
        [SerializeField] private Slider healthBar;
        private EnemyHealth _enemyHealth;

        private void Awake()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
            _enemyHealth.OnDamagedEvent += UpdateUI;
            _enemyHealth.OnDiedEvent += OnDiedEvent;
        }

        private void UpdateUI(int health, int maxHealth)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = health;
        }

        private void OnDiedEvent() => healthBar.value = 0;

        private void OnDestroy()
        {
            _enemyHealth.OnDamagedEvent -= UpdateUI;
            _enemyHealth.OnDiedEvent -= OnDiedEvent;
        }
    }
}