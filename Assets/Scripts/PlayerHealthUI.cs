using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.InitEvent += UpdateUI;
        _health.OnDamagedEvent += UpdateUI;
        _health.OnDiedEvent += OnDeath;
    }

    private void UpdateUI(int health, int maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    private void OnDeath() => healthBar.value = 0;

    private void OnDestroy()
    {
        _health.InitEvent -= UpdateUI;
        _health.OnDamagedEvent -= UpdateUI;
        _health.OnDiedEvent -= OnDeath;
    }
}