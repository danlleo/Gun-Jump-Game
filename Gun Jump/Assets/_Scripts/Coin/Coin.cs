using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class Coin : MonoBehaviour
{
    [SerializeField] private AnimationCurve _coinJumpAnimationCurve;

    private float _jumpDuration = 1f;
    private float _jumpHeight = 2f;

    private Transform _targetToMagnet;

    private void Start()
    {
        StartCoroutine(CoinJumpRoutine());
    }

    public void Initialize(Transform targetToMagnet)
    {
        _targetToMagnet = targetToMagnet;
    }

    private IEnumerator CoinJumpRoutine()
    {
        float startTime = Time.time;
        float journeyLength = _jumpDuration;

        while (Time.time - startTime < journeyLength)
        {
            float normalizedTime = (Time.time - startTime) / journeyLength;

            float newY = _coinJumpAnimationCurve.Evaluate(normalizedTime) * _jumpHeight;

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            yield return null;
        }
    }
}
