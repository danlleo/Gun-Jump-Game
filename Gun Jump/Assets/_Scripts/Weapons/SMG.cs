using UnityEngine;

public class SMG : Weapon
{
    [SerializeField] private WeaponProperties _smgProperties;

    private Rigidbody _rb;

    private float _fireRateTimeInSeconds = .15f;
    private float _fireRateTimer = 0f;

    private void Awake()
        => _rb = GetComponent<Rigidbody>();

    private void Update()
    {
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

    protected override WeaponProperties Properties => _smgProperties;

    protected override void Fire()
    {
        Projectile projectile = ProjectilePool.Instance.GetPooledObject();
        projectile.Initialize(transform.forward, _smgProperties.ProjectileSpawnPoint.position, _smgProperties.ProjectilesCanRicochet);

        ResetFireRateTimer();
    }

    private void ResetFireRateTimer()
        => _fireRateTimer = 0f;
}
