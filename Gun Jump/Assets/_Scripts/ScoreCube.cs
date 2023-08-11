using System;
using UnityEngine;

public class ScoreCube : MonoBehaviour
{
    public static event EventHandler OnProjectileHitScoreCube;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (!_canDestroy)
            return;

        if (collision.collider.TryGetComponent(out Projectile projectile))
        {
            OnProjectileHitScoreCube?.Invoke(this, EventArgs.Empty);

            Destroy(projectile.gameObject);
            Destroy(gameObject);
            EarningManager.CalculateReceivedMoneyFromScoreCube(_moneyMultiplierAmount);
        }
    }
}
