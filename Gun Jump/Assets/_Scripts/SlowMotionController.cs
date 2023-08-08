using UnityEngine;

public class SlowMotionController : Singleton<SlowMotionController>
{
    private float _originalTimeScale;

    protected override void Awake()
        => base.Awake();

    private void Start()
        => _originalTimeScale = Time.timeScale;

    private void TriggerSlowMotion()
    {
        // ...
    }
}
