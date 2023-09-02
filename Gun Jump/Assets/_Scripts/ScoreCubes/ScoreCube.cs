using UnityEngine;

[DisallowMultipleComponent]
public class ScoreCube : MonoBehaviour, IHitable
{
    [SerializeField] private Transform _cubeTopPosition;
    [SerializeField] private int _moneyMultiplierAmount;

    private bool _canDestroy = true;

    private void OnEnable()
    {
        ProjectileHitScoreCubeStaticEvent.OnProjectileHitScoreCube += ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube;
    }

    private void OnDisable()
    {
        ProjectileHitScoreCubeStaticEvent.OnProjectileHitScoreCube -= ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube;
    }

    public void OnHit(Projectile projectile)
    {
        if (!_canDestroy)
            return;

        ProjectileHitScoreCubeStaticEvent.CallProjectileHitScoreCubeEvent(_moneyMultiplierAmount, transform.position, _canDestroy);

        Destroy(gameObject);
        ProjectilePool.Instance.ReturnToPool(projectile);
        Economy.CalculateReceivedMoneyFromScoreCube(_moneyMultiplierAmount);
    }

    public void OnHit()
    {
        return;
    }

    private void ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube(ProjectileHitScoreCubeEventArgs _)
    {
        SetNonDestructible();
    }

    public Vector3 GetCubeTopPosition()
        => _cubeTopPosition.position;

    private void SetNonDestructible()
        => _canDestroy = false;
}
