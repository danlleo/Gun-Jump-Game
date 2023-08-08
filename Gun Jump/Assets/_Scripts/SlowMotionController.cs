using System.Collections;
using UnityEngine;

public class SlowMotionController : Singleton<SlowMotionController>
{
    private float _originalTimeScale;
    private float _originalFixedDeltaTime;

    private float _slowMotionInDuration = .30f;
    private float _slowMotionOutDuration = .30f;

    private Coroutine _slowMotionRoutine;

    protected override void Awake()
        => base.Awake();

    private void Start()
    {
        _originalTimeScale = Time.timeScale;
        _originalFixedDeltaTime = Time.fixedDeltaTime;
    }

    public void TriggerSlowMotion(float duration, float targetTimeScaleValue)
    {
        targetTimeScaleValue = Mathf.Clamp(targetTimeScaleValue, 0f, 1f);

        if (_slowMotionRoutine != null)
            return;

        _slowMotionRoutine = StartCoroutine(SlowMotionRoutine(duration, targetTimeScaleValue));
    }

    private IEnumerator SlowMotionRoutine(float duration, float targetTimeScaleValue)
    {
        // Slow Motion Enter
        float inTimer = 0f;

        while (inTimer <= _slowMotionInDuration)
        {
            inTimer += Time.deltaTime;
            float t = inTimer / _slowMotionInDuration;
            Time.timeScale = Mathf.Lerp(_originalTimeScale, targetTimeScaleValue, t);
            Time.fixedDeltaTime = Time.timeScale * 0.01f;

            yield return null;
        }

        // Slow Motion Stay
        yield return new WaitForSeconds(duration);

        // Slow Motion Leave
        float outTimer = 0f;

        while (outTimer <= _slowMotionOutDuration)
        {
            outTimer += Time.deltaTime;
            float t = outTimer / _slowMotionOutDuration;
            Time.timeScale = Mathf.Lerp(targetTimeScaleValue, _originalTimeScale, t);
            Time.fixedDeltaTime = Time.timeScale * 0.01f;

            yield return null;
        }

        // Ensure that properties are correctly set
        Time.fixedDeltaTime = _originalFixedDeltaTime;
        Time.timeScale = _originalTimeScale;

        _slowMotionRoutine = null;
    }
}
