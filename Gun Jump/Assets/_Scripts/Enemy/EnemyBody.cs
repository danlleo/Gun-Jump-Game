using _Scripts.Interfaces;
using _Scripts.Projectile;
using UnityEngine;

namespace _Scripts.Enemy
{
    [DisallowMultipleComponent]
    public class EnemyBody : MonoBehaviour, IHittable
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private float _ragdollBounceBackForce = 5f;
        
        public void OnHit()
        {
            _enemy.EnemyHitEvent.CallEnemyHitEvent(false);
        }

        public void OnHit(Projectile.Projectile projectile)
        {
            _enemy.EnemyHitEvent.CallEnemyHitEvent(false);
            _enemy.Rb.AddForce(projectile.transform.forward * _ragdollBounceBackForce, ForceMode.Impulse);
            
            if (projectile.CanGoThroughBodies)
                return;
            
            ProjectilePool.Instance.ReturnToPool(projectile);
        }
    }
}
