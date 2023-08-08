using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private GameObject _replacementPrefab;
    [SerializeField] private AudioClip _bangClip;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Projectile projectile))
        {
            SlowMotionController.Instance.TriggerSlowMotion(.5f, .2f);
        }

        ScreenShake.Instance.Shake();
        AudioSource.PlayClipAtPoint(_bangClip, transform.position);
        Instantiate(_replacementPrefab, transform.position, Quaternion.identity);
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
