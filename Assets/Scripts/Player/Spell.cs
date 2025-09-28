using UnityEngine;

public abstract class Spell : ScriptableObject
{
    public float Cooldown = 1;
    public string SpellName = "New Spell";

    public abstract void Cast(Vector3 castOriginPosition, Vector3 castDirection);
}