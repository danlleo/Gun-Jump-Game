using System;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemyHitEvent : MonoBehaviour
{
    public event Action<EnemyHitEvent, EnemyHitEventArgs> OnEnemyHit;

    public void CallEnemyHitEvent(bool isHeadshot)
        => OnEnemyHit?.Invoke(this, new EnemyHitEventArgs(isHeadshot));
}

public class EnemyHitEventArgs : EventArgs
{
    public bool IsHeadshot;

    public EnemyHitEventArgs(bool isHeadshot)
    {
        IsHeadshot = isHeadshot;
    }
}