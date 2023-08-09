using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Gun : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private AudioClip _shotClip;

    private Rigidbody _rb;

    private float _torque = 140f;
    private float _maxAngularVelocity = 10f;
    private float _bounceBackForce = 40f;
    private float _jumpUpForce = 2.25f;
    private float _maxHeight = 5.5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ClampAngularVelocity();

        if (PlayerInputHandler.IsMouseButtonDownThisFrame())
        {
            ShootProjectile();
            PerformRecoil();
            BounceBack();
        }
    }

    private void ClampAngularVelocity()
        => _rb.angularVelocity = new Vector3(Mathf.Clamp(_rb.angularVelocity.x, -_maxAngularVelocity, _maxAngularVelocity), 0f, 0f);

    private void ShootProjectile()
    {
        AudioSource.PlayClipAtPoint(_shotClip, transform.position);

        Projectile projectile = Instantiate(_projectilePrefab, _projectileSpawnPoint.position, Quaternion.identity);
        projectile.Initialize(transform.forward);
    }

    private void PerformRecoil()
    {
        Vector3 facingDirection = Vector3.Dot(_projectileSpawnPoint.forward, Vector3.forward) < 0
                ? Vector3.left
                : Vector3.right;

        _rb.AddTorque(facingDirection * _torque);
    }

    private void BounceBack()
    {
        float heightMultiplier = Mathf.InverseLerp(_maxHeight, 0, transform.position.y);
        
        Vector3 forceMultiplier = Vector3.Dot(Vector3.up, -transform.forward) < 0
            ? Vector3.zero
            : _jumpUpForce * heightMultiplier * Vector3.up;

        _rb.velocity = Vector3.zero;
        _rb.AddForce((-transform.forward + forceMultiplier) * _bounceBackForce);
    }
}
