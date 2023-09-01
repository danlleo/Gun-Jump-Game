using System;

public static class EnemyDiedStaticEvent
{
    public static event Action<EnemyDiedStaticEventArgs> OnEnemyDied;

    public static void CallEnemyDiedEvent(bool hasDiedOutOfHeadshot = false)
        => OnEnemyDied?.Invoke(new EnemyDiedStaticEventArgs(hasDiedOutOfHeadshot));
}

public class EnemyDiedStaticEventArgs : EventArgs
{
    public bool HasDiedOutOfHeadshot;

    public EnemyDiedStaticEventArgs(bool hasDiedOutOfHeadshot = false)
    {
        HasDiedOutOfHeadshot = hasDiedOutOfHeadshot;
    }
}
