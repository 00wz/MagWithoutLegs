using UnityEngine;

public interface IHUD
{
    void SetHealth(float value);

    void SetHealCooldown();

    void SetFireballCooldown();

    void SetTargetHealth(float value);
}
