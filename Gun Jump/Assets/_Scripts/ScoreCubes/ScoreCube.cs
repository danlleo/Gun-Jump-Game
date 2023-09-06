using _Scripts.Interfaces;
using _Scripts.Projectile;
using _Scripts.StaticEvents.ScoreCube;
using UnityEngine;

namespace _Scripts.ScoreCubes
{
    [DisallowMultipleComponent]
    public class ScoreCube : MonoBehaviour, IHittable
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

        public void OnHit(Projectile.Projectile projectile)
        {
            ProjectilePool.Instance.ReturnToPool(projectile);
        
            if (!_canDestroy)
                return;

            ProjectileHitScoreCubeStaticEvent.CallProjectileHitScoreCubeEvent(_moneyMultiplierAmount, transform.position, _canDestroy);
            Economy.Economy.CalculateReceivedMoneyFromScoreCubeAndAddToCurrentAmount(_moneyMultiplierAmount);

            Destroy(gameObject);
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
}
