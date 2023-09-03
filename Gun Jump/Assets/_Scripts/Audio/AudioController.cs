using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    [SerializeField] private AudioClipRefsSO _clipRefs;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectsSource;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        EnemyMultipleKillStreakEndedStaticEvent.OnEnemyMultipleKillStreakEnded += EnemyMultipleKillStreakEndedStaticEvent_OnEnemyMultipleKillStreakEnded;
        EnemyDiedStaticEvent.OnEnemyDied += EnemyDiedStaticEvent_OnEnemyDied;
        CoinPickUpStaticEvent.OnCoinPickUp += CoinPickUpStaticEvent_OnCoinPickUp;
    }

    private void OnDisable()
    {
        EnemyMultipleKillStreakEndedStaticEvent.OnEnemyMultipleKillStreakEnded -= EnemyMultipleKillStreakEndedStaticEvent_OnEnemyMultipleKillStreakEnded;
        EnemyDiedStaticEvent.OnEnemyDied -= EnemyDiedStaticEvent_OnEnemyDied;
        CoinPickUpStaticEvent.OnCoinPickUp -= CoinPickUpStaticEvent_OnCoinPickUp;
    }

    private void CoinPickUpStaticEvent_OnCoinPickUp()
    {
        PlaySound(_clipRefs.CoinPickUp, 0.2f);
    }

    private void EnemyDiedStaticEvent_OnEnemyDied(EnemyDiedStaticEventArgs enemyDiedStaticEventArgs)
    {
        if (enemyDiedStaticEventArgs.HasDiedOutOfHeadshot)
        {
            PlaySound(_clipRefs.Headshot, 0.3f);
        }
    }

    private void EnemyMultipleKillStreakEndedStaticEvent_OnEnemyMultipleKillStreakEnded(EnemyMultipleKillStreakEndedStaticEventArgs enemyMultipleKillStreakEndedStaticEventArgs)
    {
        switch (enemyMultipleKillStreakEndedStaticEventArgs.TotalKillCount)
        {
            case 2:
                PlaySound(_clipRefs.DoubleKill);
                break; 
            case 3:
                PlaySound((_clipRefs.TripleKill));
                break;

            default: 
                break;
        }
    }

    public void PlaySound(AudioClip audioClip, float volume = 1f)
    {
        _effectsSource.PlayOneShot(audioClip, volume);
    }
}
