﻿using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    private PlayerHealth _health;

    private void Awake()
    {
        _health = GetComponent<PlayerHealth>();
        _health.InitEvent += UpdateUI;
        _health.OnDamagedEvent += UpdateUI;
        _health.OnDiedEvent += OnDeath;
    }

    private void UpdateUI(float health, float maxHealth)
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