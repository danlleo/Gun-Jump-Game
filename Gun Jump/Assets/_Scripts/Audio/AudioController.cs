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
        ProjectileHitScoreCubeStaticEvent.OnProjectileHitScoreCube += ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube;
    }

    private void OnDisable()
    {
        EnemyMultipleKillStreakEndedStaticEvent.OnEnemyMultipleKillStreakEnded -= EnemyMultipleKillStreakEndedStaticEvent_OnEnemyMultipleKillStreakEnded;
        EnemyDiedStaticEvent.OnEnemyDied -= EnemyDiedStaticEvent_OnEnemyDied;
        CoinPickUpStaticEvent.OnCoinPickUp -= CoinPickUpStaticEvent_OnCoinPickUp;
        ProjectileHitScoreCubeStaticEvent.OnProjectileHitScoreCube -= ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube;
    }

    private void ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube(ProjectileHitScoreCubeEventArgs _)
    {
        PlaySound(_clipRefs.HitScoreCube, 0.85f);
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

        PlaySound(_clipRefs.EnemyDeathSounds, .7f);
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

    public void PlaySound(AudioClip[] audioClips, float volume = 1f)
    {
        _effectsSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length - 1)], volume);
    }
}
