using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private float damage = 0.1f;
    [SerializeField] private float attackCooldawnTime = 1f;
    [SerializeField] private float mooveSpeed = 1f;
    [SerializeField] private float heath = 1f;
    [SerializeField] private ProgressBar progressBar;
    private Transform _targetTransform;
    private IDamageable _targetDamageable;
    public event Action OnDied;
    private float _lastAttackTimestamp;
    private Rigidbody2D _rigitbody;
    private bool _isDead = false;

    void Awake()
    {
        _rigitbody = GetComponent<Rigidbody2D>();
    }

    public float GetHealth()
    {
        return heath;
    }

    public void TakeHealth(float value)
    {
        heath += value;
        progressBar.SetProgress(value);
        if (heath <= 0)
        {
            Dead();
        }
    }

    protected virtual void Dead()
    {
        _isDead = true;

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
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

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == _targetTransform)
        {
            _targetTransform = null;
            _targetDamageable = null;
            _rigitbody.linearVelocityX = 0f;
        }
    }


    void Update()
    {
        if (_isDead)
        {
            return;
        }

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
                // _rigitbody.MovePosition(
                //     _rigitbody.position +
                //     Math.Sign((_targetTransform.position - transform.position).x) * Vector2.right * mooveSpeed * Time.deltaTime);
                var targetXSpeed = Math.Sign((_targetTransform.position - transform.position).x) * mooveSpeed;
                //targetXSpeed = Mathf.MoveTowards(_rigitbody.linearVelocity.x, targetXSpeed, mooveSpeed * Time.deltaTime);
                // _rigitbody.linearVelocity = new Vector2(targetXSpeed, _rigitbody.linearVelocity.y);
                _rigitbody.AddForce(targetXSpeed * Vector2.right, ForceMode2D.Force);
            }
        }
    }

    protected virtual void AttackTarget()
    {
        _targetDamageable.TakeHealth(-damage);
        _lastAttackTimestamp = Time.time;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
