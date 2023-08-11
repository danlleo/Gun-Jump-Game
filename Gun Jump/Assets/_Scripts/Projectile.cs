using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Vector3 _direction;
    private Rigidbody _rb;

    private float _moveSpeed = 5f;
    private float _bounceAngleDeviation = 30f;

    private int _maxBounceCount = 3;
    private int _bounceCount;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
        => Move();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts.Length == 0)
            return;

        if (_bounceCount >= _maxBounceCount)
            Destroy(gameObject);

        _bounceCount++;
        Vector3 surfaceNormal = collision.contacts[0].normal;
        DoRicochet(surfaceNormal);
    }

    public void Initialize(Vector3 direction, Vector3 startPosition)
    {
        _direction = direction;
        transform.position = startPosition;
    }

    private void DoRicochet(Vector3 surfaceNormal)
    {
        _direction = Vector3.Reflect(_direction, surfaceNormal);
        _direction = Quaternion.Euler(Random.Range(-_bounceAngleDeviation, _bounceAngleDeviation), 0f, 0f) * _direction;
    }

    private void Move()
        => _rb.velocity = _direction * _moveSpeed;
}
