using System;
using UnityEngine;

public class BasePrincess : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 1f;
    [SerializeField] private float health = 1f;    public event Action OnDied;
    [SerializeField] private ProgressBar progressBar;
    private bool _isDead = false;

    public float GetHealth()
    {
        return health;
    }

    public void TakeHealth(float value)
    {
        health += value;
        progressBar.SetProgress(health/maxHealth);
        if (health <= 0)
        {
            Dead();
        }
    }


    protected virtual void Dead()
    {
        _isDead = true;

        Destroy(gameObject);
    }
}
