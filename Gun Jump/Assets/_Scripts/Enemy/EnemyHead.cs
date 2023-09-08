using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.Enemy
{
    [DisallowMultipleComponent]
    public class EnemyHead : MonoBehaviour, IHittable
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private float _ragdollBounceBackForce = 7f;
        
        public void OnHit()
        {
            _enemy.EnemyHitEvent.CallEnemyHitEvent(false);
        }

        public void OnHit(Projectile.Projectile projectile)
        {
            _enemy.EnemyHitEvent.CallEnemyHitEvent(true);
            _enemy.Rb.AddForce(projectile.transform.forward * _ragdollBounceBackForce, ForceMode.Impulse);
        }
    }
}
