using _Scripts.Audio;
using _Scripts.Interfaces;
using _Scripts.Projectile;
using UnityEngine;

namespace _Scripts.Environment.ExplosionBarrel
{
    [SelectionBase]
    [RequireComponent(typeof(CapsuleCollider))]
    public class ExplosionBarrel : MonoBehaviour, IHittable
    {
        [SerializeField] private Transform _explosionEffectPrefab;
        [SerializeField] private AudioClip _explosionAudioClip;

        private CapsuleCollider _sphereCollider;

        private void Awake()
            => _sphereCollider = GetComponent<CapsuleCollider>();

        public void OnHit(Projectile.Projectile projectile)
        {
            _sphereCollider.enabled = false;
            AudioController.Instance.PlaySound(_explosionAudioClip);
            ProjectilePool.Instance.ReturnToPool(projectile);
            Instantiate(_explosionEffectPrefab, transform.position, Quaternion.identity);
            ApplyHitOnHittableObjectInRadius(2f);
            Destroy(gameObject);
        }

        public void OnHit()
        {
            _sphereCollider.enabled = false;
            AudioController.Instance.PlaySound(_explosionAudioClip);
            Instantiate(_explosionEffectPrefab, transform.position, Quaternion.identity);
            ApplyHitOnHittableObjectInRadius(2f);
            Destroy(gameObject);
        }

        private void ApplyHitOnHittableObjectInRadius(float radius)
        {
            var hittableCollidersInRadius = Physics.OverlapSphere(transform.position, radius);

            foreach (var hittableCollider in hittableCollidersInRadius)
            {
                if (hittableCollider.TryGetComponent(out IHittable hittable))
                    hittable.OnHit();
            }
        }
    }
}
