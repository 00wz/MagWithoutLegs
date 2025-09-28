using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private float damage = 0.1f;
    [SerializeField] private float attackCooldawnTime = 1f;
    [SerializeField] private float mooveSpeed = 1f;
    private Transform _targetTransform;
    private IDamageable _targetDamageable;
    public event Action OnDied;
    private float _lastAttackTimestamp;
    private Rigidbody2D _rigitbody;

    void Awake()
    {
        _rigitbody = GetComponent<Rigidbody2D>();
    }

    public float GetHealth()
    {
        throw new NotImplementedException();
    }

    public void TakeHealth(float value)
    {
        throw new NotImplementedException();
    }

    void OnTriggerEnter(Collider other)
    {
        if (_targetTransform != null)
        {
            return;
        }

        _targetTransform = other.GetComponentInParent<Player>()?.transform
         ?? other.GetComponentInParent<BasePrincess>()?.transform;
        if (_targetTransform == null)
        {
            return;
        }
        _targetDamageable = _targetTransform.GetComponentInChildren<IDamageable>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == _targetTransform)
        {
            _targetTransform = null;
            _targetDamageable = null;
        }
    }


    void Update()
    {
        if (_targetTransform != null)
        {
            if (Vector3.Distance(_targetTransform.position, transform.position) < attackRadius)
            {
                if (_lastAttackTimestamp + attackCooldawnTime < Time.time)
                {
                    AttackTarget();
                }
            }
            else
            {
                _rigitbody.MovePosition(
                    _rigitbody.position + Math.Sign((_targetTransform.position - transform.position).x) * Vector2.right * mooveSpeed);
            }
        }
    }

    protected virtual void AttackTarget()
    {
        _targetDamageable.TakeHealth(damage);
        _lastAttackTimestamp = Time.time;
    }
}
