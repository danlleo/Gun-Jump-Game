using System.Collections;
using _Scripts.StaticEvents.Coin;
using UnityEngine;

namespace _Scripts.Coin
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(TrailRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    [DisallowMultipleComponent]
    public class Coin : MonoBehaviour
    {
        [Space(10)]
        [Header("Coin Force Settings")]
        [Space(5)]

        [SerializeField] private float _minAppliedForce = 3.5f;
        [SerializeField] private float _maxAppliedForce = 7.5f;

        [Space(10)]
        [Header("Coin Move Settings")]
        [Space(5)]

        [SerializeField] private float _waitBeforeMovingTimeInSeconds = 2.1f;
        [SerializeField] private float _moveToWeaponTimeInSeconds = 2.25f;

        private Rigidbody _rb;
        private TrailRenderer _trailRenderer;
        private MeshCollider _meshCollider;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _trailRenderer = GetComponent<TrailRenderer>();
            _meshCollider = GetComponent<MeshCollider>();
        }

        private void OnEnable()
        {
            EnableCoinRigidbody();
            EnableCollider();
        }

        private void OnDisable()
        {
            ClearTrailRenderer();
        }

        public void Initialize(Vector3 spawnPosition, Quaternion spawnRotation, Transform objectToMoveTransform)
        {
            transform.SetPositionAndRotation(spawnPosition, spawnRotation);
            StartCoroutine(MoveToWeaponRoutine(objectToMoveTransform));
        }
    
        public void ApplyRandomForceToCoin()
        {
            Vector3 randomDirection = Random.insideUnitSphere.normalized;
            _rb.AddForce(randomDirection * Random.Range(_minAppliedForce, _maxAppliedForce), ForceMode.Impulse);
        }

        private IEnumerator MoveToWeaponRoutine(Transform objectToMoveTransform)
        {
            yield return new WaitForSeconds(_waitBeforeMovingTimeInSeconds);
     
            DisableCoinRigidbody();
            DisableCollider();

            Vector3 startPosition = transform.position;
            float timer = 0f;

            while (timer <  _moveToWeaponTimeInSeconds)
            {
                timer += Time.deltaTime;
                float t = timer / _moveToWeaponTimeInSeconds;

                transform.position = Vector3.Lerp(startPosition, objectToMoveTransform.position, t);

                yield return null;
            }

            CoinPickUpStaticEvent.CallCoinPickUpEvent();
            CoinPool.Instance.ReturnToPool(this);
        }

        private void ClearTrailRenderer()
            => _trailRenderer.Clear();

        private void DisableCoinRigidbody()
            => _rb.isKinematic = true;

        private void EnableCoinRigidbody()
            => _rb.isKinematic = false;

        private void DisableCollider()
            => _meshCollider.enabled = false;

        private void EnableCollider()
            => _meshCollider.enabled = true;
    }
}
