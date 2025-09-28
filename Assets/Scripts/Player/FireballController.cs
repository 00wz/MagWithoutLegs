using System;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionDamage;
    [SerializeField] private LayerMask explosionLayerMask;

    private void Update()
    {
        rb.linearVelocity = transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Boom!");
        
        ProcessExplosionToAllRigidbodies(other.contacts[0].point);
        
        Destroy(gameObject);
    }

    private void ProcessExplosionToAllRigidbodies(Vector3 position)
    {
        var position2D = new Vector2(position.x, position.y);
        var collisionsInRadius = Physics2D.OverlapCircleAll(position2D, explosionRadius, explosionLayerMask);
        
        foreach (var collision in collisionsInRadius)
        {
            var closestPointPosition = collision.ClosestPoint(position2D);
            var multiplier = 1 - Vector2.Distance(position2D, closestPointPosition) / explosionRadius;

            var force = (closestPointPosition - position2D).normalized * multiplier * explosionForce;
            collision.attachedRigidbody.AddForce(force, ForceMode2D.Impulse);
            
            var damageable = collision.gameObject.GetComponent<IDamageable>();
            damageable?.TakeHealth(-explosionDamage * multiplier);
        }
    }
}