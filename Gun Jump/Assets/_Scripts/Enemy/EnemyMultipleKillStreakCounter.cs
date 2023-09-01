using System.Collections;
using UnityEngine;

public class EnemyMultipleKillStreakCounter : MonoBehaviour
{
    private const float TIME_BEFORE_STREAK_ENDS_IN_SECONDS = .5f;

    private int _enemyKillStreakCount;

    private Coroutine _killStreakCountdownInSecondsCoroutine;

    private void OnEnable()
    {
        EnemyDiedStaticEvent.OnEnemyDied += EnemyDiedStaticEvent_OnEnemyDied;
    }

    private void OnDisable()
    {
        EnemyDiedStaticEvent.OnEnemyDied -= EnemyDiedStaticEvent_OnEnemyDied;
    }

    private void EnemyDiedStaticEvent_OnEnemyDied(EnemyDiedStaticEventArgs enemyDiedStaticEventArgs)
    {
        if (_killStreakCountdownInSecondsCoroutine != null)
            StopCoroutine(KillStreakCountdownInSecondsRoutine());

        StartCoroutine(KillStreakCountdownInSecondsRoutine());
    }

    private IEnumerator KillStreakCountdownInSecondsRoutine()
    {
        IncreaseKillStreakCount();

        float killStreakCountdownTimer = TIME_BEFORE_STREAK_ENDS_IN_SECONDS;

        while (killStreakCountdownTimer > 0)
        {
            killStreakCountdownTimer -= Time.deltaTime;
            yield return null;
        }

        EndKillStreak();
    }

    private void EndKillStreak()
    {
        EnemyMultipleKillStreakEndedStaticEvent.CallEnemyMultipleKillStreakEndedStaticEvent(_enemyKillStreakCount);
        _killStreakCountdownInSecondsCoroutine = null;
        _enemyKillStreakCount = 0;
    }

    private void IncreaseKillStreakCount()
        => _enemyKillStreakCount++;
}
