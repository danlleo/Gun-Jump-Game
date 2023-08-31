using UnityEngine;

[RequireComponent (typeof(EnemyHitEvent))]
[DisallowMultipleComponent]
public class Enemy : MonoBehaviour
{
    [HideInInspector] public EnemyHitEvent EnemyHitEvent;

    [Tooltip("Populate with the collider responsible to detect bodyshots")]
    [SerializeField] private CapsuleCollider _enemyBodyCapsuleCollider;

    [Tooltip("Populate with the collider responsible to detect headshots")]
    [SerializeField] private SphereCollider _enemyHeadSphereCollider;

    private bool _isDead;

    private void Awake()
    {
        EnemyHitEvent = GetComponent<EnemyHitEvent>();
    }

    private void OnEnable()
    {
        EnemyHitEvent.OnEnemyHit += EnemyHitEvent_OnEnemyHit;
    }

    private void OnDisable()
    {
        EnemyHitEvent.OnEnemyHit -= EnemyHitEvent_OnEnemyHit;
    }

    private void EnemyHitEvent_OnEnemyHit(EnemyHitEvent enemyHitEvent, EnemyHitEventArgs enemyHitEventArgs)
    {
        if (_isDead) 
            return;

        _isDead = true;

        if (enemyHitEventArgs.IsHeadshot)
            print("Headshot");
        else
            print("Bodyshot");

        _enemyBodyCapsuleCollider.enabled = false;
        _enemyHeadSphereCollider.enabled = false;
    }
}
