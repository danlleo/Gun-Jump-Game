using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private AudioClip _shotClip;
    [SerializeField] private WeaponDetailsSO _pistolDetails;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private ParticleSystem _muzzleFlashEffect;

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

    protected override WeaponDetailsSO WeaponDetails => _pistolDetails;

    protected override Transform WeaponProjectileSpawnPoint => _projectileSpawnPoint;

    protected override void Fire()
    {
        _muzzleFlashEffect.Play();
        SoundManager.Instance.PlaySound(_shotClip);

        Projectile projectile = ProjectilePool.Instance.GetPooledObject();
        projectile.Initialize(transform.forward, _projectileSpawnPoint.position, _pistolDetails.ProjectilesCanRicochet);

        if (Physics.Raycast(_projectileSpawnPoint.position, _projectileSpawnPoint.forward, out RaycastHit hitInfo, float.MaxValue))
        {
            if (hitInfo.collider.TryGetComponent(out Enemy enemy))
            {
                SlowMotionController.Instance.TriggerSlowMotion(.3f, .2f);
            }
        }
    }
}
