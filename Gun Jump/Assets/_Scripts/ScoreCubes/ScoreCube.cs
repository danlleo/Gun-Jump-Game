using System;
using UnityEngine;

public class ScoreCube : MonoBehaviour, IHitable
{
    public static event EventHandler OnProjectileHitScoreCube;

    [SerializeField] private Transform _cubeTopPosition;
    [SerializeField] int _moneyMultiplierAmount;

    private bool _canDestroy = true;

    private void OnEnable()
    {
        OnProjectileHitScoreCube += ScoreCube_OnProjectileHitScoreCube;
    }

    private void OnDisable()
    {
        OnProjectileHitScoreCube -= ScoreCube_OnProjectileHitScoreCube;
    }

    private void ScoreCube_OnProjectileHitScoreCube(object sender, EventArgs e)
    {
        _canDestroy = false;
    }

    public void OnHit(Projectile projectile)
    {
        if (!_canDestroy)
            return;

        Destroy(gameObject);
        ProjectilePool.Instance.ReturnToPool(projectile);
        Economy.CalculateReceivedMoneyFromScoreCube(_moneyMultiplierAmount);
    }

    public void OnHit()
    {
        return;
    }

    public Vector3 GetCubeTopPosition()
        => _cubeTopPosition.position;
}
