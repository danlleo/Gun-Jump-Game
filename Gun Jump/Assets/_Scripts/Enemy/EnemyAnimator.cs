using UnityEngine;

namespace _Scripts.Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Enemy))]
    [DisallowMultipleComponent]
    public class EnemyAnimator : MonoBehaviour
    {
        private Animator _animator;
        private Enemy _enemy;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _enemy = GetComponent<Enemy>();
        }

        private void OnEnable()
        {
            _enemy.EnemyHitEvent.OnEnemyHit += EnemyHitEvent_OnEnemyHit;
        }

        private void OnDisable()
        {
            _enemy.EnemyHitEvent.OnEnemyHit -= EnemyHitEvent_OnEnemyHit;
        }

        private void EnemyHitEvent_OnEnemyHit(EnemyHitEvent enemyHitEvent, EnemyHitEventArgs enemyHitEventArgs)
        {
            _animator.SetTrigger(EnemyAnimationParams.OnEnemyHit);
        }
    }
}
