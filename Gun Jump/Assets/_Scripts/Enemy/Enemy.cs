using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour, IHittable
{
    public event EventHandler OnEnemyHit;

    [SerializeField] private CapsuleCollider _enemyCapsuleCollider;

    private void Awake()
        => _enemyCapsuleCollider = GetComponent<CapsuleCollider>();

    public void OnHit(Projectile projectile)
    {
        ProjectilePool.Instance.ReturnToPool(projectile);
        OnEnemyHit?.Invoke(this, EventArgs.Empty);
        _enemyCapsuleCollider.enabled = false;
    }

    public void OnHit()
    {
        OnEnemyHit?.Invoke(this, EventArgs.Empty);
        _enemyCapsuleCollider.enabled = false;
    }
}
