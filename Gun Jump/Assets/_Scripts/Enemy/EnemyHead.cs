using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.Enemy
{
    [DisallowMultipleComponent]
    public class EnemyHead : MonoBehaviour, IHittable
    {
        [SerializeField] private Enemy _enemy;

        public void OnHit()
        {
            _enemy.EnemyHitEvent.CallEnemyHitEvent(false);
        }

        public void OnHit(Projectile.Projectile projectile)
        {
            _enemy.EnemyHitEvent.CallEnemyHitEvent(true);
        }
    }
}
