using UnityEngine;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(Enemy))]
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
        _enemy.OnEnemyHit += Enemy_OnEnemyHit;
    }

    private void OnDisable()
    {
        _enemy.OnEnemyHit -= Enemy_OnEnemyHit;
    }

    private void Enemy_OnEnemyHit(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(EnemyAnimationParams.OnEnemyHit);
    }
}
