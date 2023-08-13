using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectsSource;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(AudioClip audioClip)
    {
        _effectsSource.PlayOneShot(audioClip);
    }
}
