using _Scripts.Audio;
using _Scripts.Enums;
using _Scripts.Misc;
using _Scripts.Projectile;
using _Scripts.Utilities.GameManager;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class Pistol : Weapon
    {
        [SerializeField] private AudioClip _shotClip;
        [SerializeField] private WeaponDetailsSO _pistolDetails;
        [SerializeField] private WeaponSO _weaponSO;
        [SerializeField] private Transform _projectileSpawnPoint;
        [SerializeField] private ParticleSystem _muzzleFlashEffect;

        private Rigidbody _rb;

        private void Awake()
            => _rb = GetComponent<Rigidbody>();

        private void Update()
        {
            ClampAngularVelocity();
            
            if (GameManager.Instance.CurrentGameState != GameState.PLAYING_LEVEL)
                return;
            
            if (!PlayerInputHandler.IsMouseButtonDownThisFrame()) return;
            if (PlayerInputHandler.IsMouseOverInteractableUIElement())
                return;

            Fire();
            ApplyTorque();
            BounceBack();
        }

        protected override Rigidbody RB => _rb;

        protected override WeaponDetailsSO WeaponDetails => _pistolDetails;
        public override WeaponSO WeaponSO => _weaponSO;

        protected override Transform WeaponProjectileSpawnPoint => _projectileSpawnPoint;

        protected override void Fire()
        {
            base.Fire();

            _muzzleFlashEffect.Play();
            AudioController.Instance.PlaySound(_shotClip, .655f);

            var projectile = ProjectilePool.Instance.GetPooledObject();
            projectile.Initialize(transform.forward, _projectileSpawnPoint.position, _pistolDetails.ProjectilesCanRicochet, _pistolDetails.ProjectilesCanGoTroughBodies);
        }
    }
}
