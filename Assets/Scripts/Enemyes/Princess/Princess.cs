using System;
using UnityEngine;

public class BasePrincess : MonoBehaviour, IDamageable
{
    public event Action OnDied;

    public float GetHealth()
    {
        throw new NotImplementedException();
    }

    public void TakeHealth(float value)
    {
        throw new NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
