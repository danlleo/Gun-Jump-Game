using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private AudioClip _shotClip;
    [SerializeField] private WeaponProperties _pistolProperties;

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

    protected override WeaponProperties Properties => _pistolProperties;

    protected override void Fire()
    {
        AudioSource.PlayClipAtPoint(_shotClip, transform.position);

        Projectile projectile = Instantiate(_projectilePrefab, _pistolProperties.ProjectileSpawnPoint.position, Quaternion.identity);
        projectile.Initialize(transform.forward);
    }
}
