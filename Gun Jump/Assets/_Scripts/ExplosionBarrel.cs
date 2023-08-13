using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class ExplosionBarrel : MonoBehaviour, IHittable
{
    [SerializeField] private Transform _explosionEffectPrefab;
    [SerializeField] private AudioClip _explosionAudioClip;

    private CapsuleCollider _sphereCollider;

    private void Awake()
        => _sphereCollider = GetComponent<CapsuleCollider>();

    public void OnHit(Projectile projectile)
    {
        _sphereCollider.enabled = false;
        SoundManager.Instance.PlaySound(_explosionAudioClip);
        ProjectilePool.Instance.ReturnToPool(projectile);
        Instantiate(_explosionEffectPrefab, transform.position, Quaternion.identity);
        ApplyHitOnHittableObjectInRadius(2f);
        Destroy(gameObject);
    }

    public void OnHit()
    {
        _sphereCollider.enabled = false;
        SoundManager.Instance.PlaySound(_explosionAudioClip);
        Instantiate(_explosionEffectPrefab, transform.position, Quaternion.identity);
        ApplyHitOnHittableObjectInRadius(2f);
        Destroy(gameObject);
    }

    private void ApplyHitOnHittableObjectInRadius(float radius)
    {
        Collider[] hittableCollidersInRadius = Physics.OverlapSphere(transform.position, radius);

        foreach (var hittableCollider in hittableCollidersInRadius)
        {
            if (hittableCollider.TryGetComponent(out IHittable hittable))
                hittable.OnHit();
        }
    }
}
