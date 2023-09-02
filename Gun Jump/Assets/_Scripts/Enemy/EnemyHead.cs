using UnityEngine;

[DisallowMultipleComponent]
public class EnemyHead : MonoBehaviour, IHittable
{
    [SerializeField] private Enemy _enemy;

    public void OnHit()
    {
        _enemy.EnemyHitEvent.CallEnemyHitEvent(false);
    }

    public void OnHit(Projectile projectile)
    {
        _enemy.EnemyHitEvent.CallEnemyHitEvent(true);
    }
}
