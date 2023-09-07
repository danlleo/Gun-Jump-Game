using _Scripts.Enums;
using _Scripts.Misc;
using _Scripts.Projectile;
using _Scripts.Utilities.GameManager;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class SMG : Weapon
    {
        [SerializeField] private WeaponDetailsSO _smgDetails;
        [SerializeField] private WeaponSO _weaponSO;
        [SerializeField] private Transform _projectileSpawnPoint;

        private Rigidbody _rb;

        private float _fireRateTimeInSeconds = .15f;
        private float _fireRateTimer = 0f;

        private void Awake()
            => _rb = GetComponent<Rigidbody>();

        private void Update()
        {
            ClampAngularVelocity();
            
            if (GameManager.Instance.CurrentGameState != GameState.PLAYING_LEVEL)
                return;

            _fireRateTimer += Time.deltaTime;

            if (_fireRateTimer < _fireRateTimeInSeconds)
                return; 

            if (PlayerInputHandler.IsMouseButtonDownThisFrame())
            {
                if (PlayerInputHandler.IsMouseOverInteractableUIElement())
                    return;

                ApplyTorque();
                BounceBack();
            }
            else if (PlayerInputHandler.IsMouseButtonHeldThisFrame())
            {
                if (PlayerInputHandler.IsMouseOverInteractableUIElement())
                    return;

                Fire();
            }
        }

        protected override Rigidbody RB => _rb;

        protected override WeaponDetailsSO WeaponDetails => _smgDetails;
        public override WeaponSO WeaponSO => _weaponSO;

        protected override Transform WeaponProjectileSpawnPoint => _projectileSpawnPoint;

        protected override void Fire()
        {
            base.Fire();

            var projectile = ProjectilePool.Instance.GetPooledObject();
            projectile.Initialize(transform.forward, _projectileSpawnPoint.position, _smgDetails.ProjectilesCanRicochet, _smgDetails.ProjectilesCanGoTroughBodies);

            ResetFireRateTimer();
        }

        private void ResetFireRateTimer()
            => _fireRateTimer = 0f;
    }
}
