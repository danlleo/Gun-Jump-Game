using _Scripts.Audio;
using _Scripts.Interfaces;
using _Scripts.Utilities;
using UnityEngine;

namespace _Scripts.Projectile
{
    [RequireComponent(typeof(TrailRenderer))]
    [DisallowMultipleComponent]
    public class Projectile : MonoBehaviour
    {
        public bool CanGoThroughBodies { get; private set; }

        [SerializeField] private AudioClip _impactClip;
        [SerializeField] private Transform _hitCheckPointTransform;
        [SerializeField] private Transform _hitImpactEffectPrefab;
        [SerializeField] private LayerMask _layerMask;

        private Vector3 _direction;
        private TrailRenderer _trailRenderer;

        private float _moveSpeed = 5f;
        
        private readonly float _bounceAngleDeviation = 30f;
        private readonly float _hitCheckDistance = .15f;

        private readonly int _maxBounceCount = 3;
        private int _bounceCount;

        private bool _canRicochet;

        private void Awake()
            => _trailRenderer = GetComponent<TrailRenderer>();

        private void OnDisable()
        {
            ClearTrailRenderer();
            ResetBounceCount();
        }

        private void Update()
        {
            CheckForHit();
            Move();
            CheckScreenBoundaries();
        }

        public void Initialize(Vector3 direction, Vector3 startPosition, bool canRicochet, bool canGoTroughBodies, float moveSpeed)
        {
            _direction = direction;
            transform.position = startPosition;
            _canRicochet = canRicochet;
            CanGoThroughBodies = canGoTroughBodies;
            _moveSpeed = moveSpeed;
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
            if (!Physics.Raycast(_hitCheckPointTransform.position, _direction, out RaycastHit hitInfo, _hitCheckDistance, _layerMask))
                return;

            InstantiateHitImpactEffect(hitInfo.point, hitInfo.normal);
            AudioController.Instance.PlaySound(_impactClip);

            // If we hit another projectile then ignore 
            if (hitInfo.collider.TryGetComponent(out Projectile _))
                return;

            if (hitInfo.collider.TryGetComponent(out IHittable hittable))
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
            if (!HelperUtilities.IsObjectWithingScreenBoundaries(transform.position))
            {
                ProjectilePool.Instance.ReturnToPool(this);
            }
        }

        private void ClearTrailRenderer()
            => _trailRenderer.Clear();

        private void ResetBounceCount()
            => _bounceCount = 0;
    }
}