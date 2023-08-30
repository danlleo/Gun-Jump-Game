using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private AudioClip _shotClip;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private WeaponDetailsSO _shotgunDetails;
    [SerializeField] private ParticleSystem _muzzleFlashEffect;
    [SerializeField] private Transform _projectileSpawnPoint;

    private Rigidbody _rb;

    private int _maxBulletSpawnCount = 2;
    private float _normalizedProjectileSpreadAngle = .1f;

    private void Awake()
        => _rb = GetComponent<Rigidbody>();

    private void Update()
    {
        ClampAngularVelocity();

        if (PlayerInputHandler.IsMouseButtonDownThisFrame())
        {
            Fire();
            ApplyTorque();
            BounceBack();
        }
    }

    protected override Rigidbody RB => _rb;

    protected override WeaponDetailsSO WeaponDetails => _shotgunDetails;

    protected override Transform WeaponProjectileSpawnPoint => _projectileSpawnPoint;

    protected override void Fire()
    {
        _muzzleFlashEffect.Play();
        SoundManager.Instance.PlaySound(_shotClip);

        // Spawn specific bullet amount
        for (int i = 0; i < _maxBulletSpawnCount; i++)
        {
            Projectile projectile = ProjectilePool.Instance.GetPooledObject();
            projectile.Initialize(
                transform.forward + transform.up * (i % 2 == 0 ? _normalizedProjectileSpreadAngle : -_normalizedProjectileSpreadAngle),
                _projectileSpawnPoint.position,
                _shotgunDetails.ProjectilesCanRicochet
            );
        }
    }
}
