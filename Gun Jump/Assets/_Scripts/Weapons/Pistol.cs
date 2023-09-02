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
        if (GameManager.Instance.CurrentGameState == GameState.GameEnded)
            return;

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
        base.Fire();

        _muzzleFlashEffect.Play();
        AudioController.Instance.PlaySound(_shotClip);

        Projectile projectile = ProjectilePool.Instance.GetPooledObject();
        projectile.Initialize(transform.forward, _projectileSpawnPoint.position, _pistolDetails.ProjectilesCanRicochet);
    }
}
