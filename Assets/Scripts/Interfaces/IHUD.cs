using UnityEngine;

public interface IHUD
{
    void SetHealth(float value);

    void SetHealCooldown(float value);

    void SetFireballCooldown(float value);

    void SetTargetHealth(float value);
}
