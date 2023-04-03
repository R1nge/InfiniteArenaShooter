using System;
using UnityEngine;

public abstract class Health : MonoBehaviour, IDamageable
{
    [SerializeField] protected int maxHealth;
    private int _health;

    public event Action<int, int> InitEvent;
    public event Action<int, int> OnDamagedEvent;
    public event Action OnDiedEvent;

    protected virtual void Awake() => _health = maxHealth;

    private void Start() => InitEvent?.Invoke(_health, maxHealth);

    public void TakeDamage(int amount)
    {
        _health -= amount;
        
        if (_health <= 0)
        {
            OnDiedEvent?.Invoke();
        }
        else
        {
            OnDamagedEvent?.Invoke(_health, maxHealth);
        }
    }
}