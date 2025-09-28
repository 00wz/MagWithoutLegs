using UnityEngine;

public interface IDamageable
{
    void TakeHealth(float value);

    float GetHealth();
}
