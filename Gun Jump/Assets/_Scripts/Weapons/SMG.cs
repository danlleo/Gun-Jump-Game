using UnityEngine;

public class SMG : Weapon
{
    [SerializeField] private WeaponDetailsSO _smgDetails;
    [SerializeField] private Transform _projectileSpawnPoint;

    private Rigidbody _rb;

    private float _fireRateTimeInSeconds = .15f;
    private float _fireRateTimer = 0f;

    private void Awake()
        => _rb = GetComponent<Rigidbody>();

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameState.GameStarted)
            return;

        _fireRateTimer += Time.deltaTime;
    
        if (PlayerInputHandler.IsMouseButtonHeldThisFrame())
        {
            if (_fireRateTimer >= _fireRateTimeInSeconds)
            {
                Fire();
                ApplyTorque();
                BounceBack();
            }
        }
    }

    protected override Rigidbody RB => _rb;

    protected override WeaponDetailsSO WeaponDetails => _smgDetails;

    protected override Transform WeaponProjectileSpawnPoint => _projectileSpawnPoint;

    protected override void Fire()
    {
        base.Fire();

        Projectile projectile = ProjectilePool.Instance.GetPooledObject();
        projectile.Initialize(transform.forward, _projectileSpawnPoint.position, _smgDetails.ProjectilesCanRicochet);

        ResetFireRateTimer();
    }

    private void ResetFireRateTimer()
        => _fireRateTimer = 0f;
}
