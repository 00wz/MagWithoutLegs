using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Fireball")]
public class Fireball : Spell
{
    [SerializeField] private FireballController _prefab;
    
    public override void Cast(Vector3 castOriginPosition, Vector3 castDirection)
    {
        var instance = Instantiate(_prefab, castOriginPosition, Quaternion.identity);
        instance.transform.forward = castDirection;
    }
}