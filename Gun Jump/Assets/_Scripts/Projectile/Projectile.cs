using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private AudioClip _impactClip;
    [SerializeField] private Transform _hitCheckPointTransform;
    [SerializeField] private Transform _hitImpactEffectPrefab;

    private Vector3 _direction;
    private Camera _camera;

    private float _moveSpeed = 5f;
    private float _bounceAngleDeviation = 30f;
    private float _hitCheckDistance = .15f;

    private int _maxBounceCount = 3;
    private int _bounceCount;

    private int _screenWidth;
    private int _screenHeight;

    private bool _canRicochet;

    private void Awake()
    {
        _camera = Camera.main;
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
    }

    private void OnDisable()
        => _bounceCount = 0;

    private void Update()
    {
        CheckScreenBoundaries();
        CheckForHit();
        Move();
    }

    public void Initialize(Vector3 direction, Vector3 startPosition, bool canRicochet)
    {
        _direction = direction;
        transform.position = startPosition;
        _canRicochet = canRicochet;
    }

    private void DoRicochet(Vector3 surfaceNormal)
    {
        if (_bounceCount >= _maxBounceCount)
            ProjectilePool.Instance.ReturnToPool(this);

        _bounceCount++;
        _direction = Vector3.Reflect(_direction, surfaceNormal);
        _direction = Quaternion.Euler(Random.Range(-_bounceAngleDeviation, _bounceAngleDeviation), 0f, 0f) * _direction;
    }

    private void Move()
        => transform.Translate(_moveSpeed * Time.deltaTime * _direction);

    private void CheckForHit()
    {
        if (!Physics.Raycast(_hitCheckPointTransform.position, _direction, out RaycastHit hitInfo, _hitCheckDistance))
            return;

        InstantiateHitImpactEffect(hitInfo.point, hitInfo.normal);
        AudioController.Instance.PlaySound(_impactClip);

        if (hitInfo.collider.TryGetComponent(out Projectile _))
            return;

        if (hitInfo.collider.TryGetComponent(out IHitable hittable))
        {
            hittable.OnHit(this);
            return;
        }

        if (!_canRicochet)
        {
            ProjectilePool.Instance.ReturnToPool(this);
            return;
        }

        DoRicochet(hitInfo.normal);
    }

    private void InstantiateHitImpactEffect(Vector3 spawnPosition, Vector3 normal)
        => Instantiate(_hitImpactEffectPrefab, spawnPosition, Quaternion.LookRotation(normal));

    private void CheckScreenBoundaries()
    {
        Vector3 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if (Mathf.Abs(screenPosition.x) > _screenWidth || Mathf.Abs(screenPosition.y) > _screenHeight)
        {
            ProjectilePool.Instance.ReturnToPool(this);
        }
    }
}