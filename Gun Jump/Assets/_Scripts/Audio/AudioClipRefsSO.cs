using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipRefs", menuName = "Scriptable Objects/Audio/AudioClipRefs")]
public class AudioClipRefsSO : ScriptableObject
{
    #region ENEMY AUDIO

    [Space(10)]
    [Header("Enemy Audio")]
    [Space(5)]

    public AudioClip DoubleKill;
    public AudioClip TripleKill;
    public AudioClip Headshot;
    public AudioClip[] EnemyDeathSounds;

    #endregion

    #region ENVIRONMENT AUDIO

    [Space(10)]
    [Header("Environment Audio")]
    [Space(5)]

    public AudioClip CoinPickUp;
    public AudioClip HitScoreCube;

    #endregion
}
