using System;

public static class EnemyMultipleKillStreakEndedStaticEvent
{
    public static event Action<EnemyMultipleKillStreakEndedStaticEventArgs> OnEnemyMultipleKillStreakEnded;

    public static void CallEnemyMultipleKillStreakEndedStaticEvent(int totalKillCount)
    {
        OnEnemyMultipleKillStreakEnded?.Invoke(new EnemyMultipleKillStreakEndedStaticEventArgs(totalKillCount));
    }
}

public class EnemyMultipleKillStreakEndedStaticEventArgs : EventArgs
{
    public int TotalKillCount;

    public EnemyMultipleKillStreakEndedStaticEventArgs(int totalKillCount)
    {
        TotalKillCount = totalKillCount;
    }
}
