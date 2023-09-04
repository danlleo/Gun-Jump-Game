using UnityEngine;

[DisallowMultipleComponent]
public class EnemyBody : MonoBehaviour, IHittable
{
    [SerializeField] private Enemy _enemy;

    public void OnHit()
    {
        _enemy.EnemyHitEvent.CallEnemyHitEvent(false);
    }

    public void OnHit(Projectile projectile)
    {
        _enemy.EnemyHitEvent.CallEnemyHitEvent(false);

        if (projectile.CanGoThroughBodies)
            return;

        ProjectilePool.Instance.ReturnToPool(projectile);
    }
}
