using _Scripts.Interfaces;
using _Scripts.Projectile;
using UnityEngine;

namespace _Scripts.Enemy
{
    [DisallowMultipleComponent]
    public class EnemyBody : MonoBehaviour, IHittable
    {
        [SerializeField] private Enemy _enemy;

        public void OnHit()
        {
            _enemy.EnemyHitEvent.CallEnemyHitEvent(false);
        }

        public void OnHit(Projectile.Projectile projectile)
        {
            _enemy.EnemyHitEvent.CallEnemyHitEvent(false);

            if (projectile.CanGoThroughBodies)
                return;

            ProjectilePool.Instance.ReturnToPool(projectile);
        }
    }
}
