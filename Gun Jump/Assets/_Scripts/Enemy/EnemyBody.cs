using UnityEngine;

[DisallowMultipleComponent]
public class EnemyBody : MonoBehaviour, IHitable
{
    [SerializeField] private Enemy _enemy;

    public void OnHit()
    {
        _enemy.EnemyHitEvent.CallEnemyHitEvent(false);
    }

    public void OnHit(Projectile projectile)
    {
        _enemy.EnemyHitEvent.CallEnemyHitEvent(false);
        ProjectilePool.Instance.ReturnToPool(projectile);
    }
}
