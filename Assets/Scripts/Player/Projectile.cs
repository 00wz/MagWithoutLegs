using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Projectile")]
public class Projectile : Spell
{
    [SerializeField] private ProjectileController _prefab;
    
    public override void Cast(Vector3 castOriginPosition, Vector3 castDirection)
    {
        var instance = Instantiate(_prefab, castOriginPosition, Quaternion.identity);
        instance.transform.forward = castDirection;
    }
}