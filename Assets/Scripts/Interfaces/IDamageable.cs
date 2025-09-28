using System;
using UnityEngine;

public interface IDamageable
{
    void TakeHealth(float value);

    float GetHealth();

    event Action OnDied;
}
