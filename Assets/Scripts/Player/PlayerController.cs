using System;
using UnityEngine;

public sealed class PlayerController : MonoBehaviour, IDamageable
{
    private float _health;

    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private IHUD _hud;
    
    public void TakeHealth(float value)
    {
        _health = Mathf.Clamp(_health + value, 0, _maxHealth);
        _hud.SetHealth(_health);
    }

    public float GetHealth() => _health;
    
    public event Action OnDied;

    private void Awake()
    {
        _health = _maxHealth;
    }
    
    private void Update()
    {
        
    }

    private void CheckForDeath()
    {
        if (_health == 0)
            OnDied?.Invoke();
    }
}
