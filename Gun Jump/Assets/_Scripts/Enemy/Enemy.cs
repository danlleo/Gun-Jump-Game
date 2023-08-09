using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour
{
    public event EventHandler OnEnemyHit;

    [SerializeField] private CapsuleCollider _enemyCapsuleCollider;

    private void Awake()
    {
        _enemyCapsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Projectile projectile))
        {
            Destroy(projectile.gameObject);
            OnEnemyHit?.Invoke(this, EventArgs.Empty);
            _enemyCapsuleCollider.enabled = false;
        }
    }
}
