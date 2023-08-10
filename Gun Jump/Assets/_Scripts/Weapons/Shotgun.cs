using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private WeaponProperties _shotgunProperties;

    private Rigidbody _rb;

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
        Projectile projectile = Instantiate(_projectilePrefab, _shotgunProperties.ProjectileSpawnPoint.position, Quaternion.identity);
        projectile.Initialize(transform.forward);
    }
}
