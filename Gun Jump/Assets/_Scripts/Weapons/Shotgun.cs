using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private AudioClip _shotClip;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private WeaponProperties _shotgunProperties;
    [SerializeField] private ParticleSystem _muzzleFlashEffect;

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

    protected override WeaponProperties Properties => _shotgunProperties;

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
                _shotgunProperties.ProjectileSpawnPoint.position,
                _shotgunProperties.ProjectilesCanRicochet
            );
        }
    }
}
