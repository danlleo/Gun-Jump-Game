using UnityEngine;

[DisallowMultipleComponent]
public class ActionTextContainer : MonoBehaviour
{
    [SerializeField] private ActionText _actionTextPrefab;

    private void OnEnable()
    {
        EnemyDiedStaticEvent.OnEnemyDied += EnemyDiedStaticEvent_OnEnemyDied;
        ProjectileHitScoreCubeStaticEvent.OnProjectileHitScoreCube += ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube;
    }

    private void OnDisable()
    {
        EnemyDiedStaticEvent.OnEnemyDied -= EnemyDiedStaticEvent_OnEnemyDied;
        ProjectileHitScoreCubeStaticEvent.OnProjectileHitScoreCube -= ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube;
    }

    private void ProjectileHitScoreCubeStaticEvent_OnProjectileHitScoreCube(ProjectileHitScoreCubeEventArgs projectileHitScoreCubeEventArgs)
    {
        if (projectileHitScoreCubeEventArgs.CanDestroy)
            InstantiateScoreCubeText($"X{projectileHitScoreCubeEventArgs.MoneyMultiplierAmount}", projectileHitScoreCubeEventArgs.ScoreCubeWorldPosition + new Vector3(0f, .3f, -1f));
    }

    private void EnemyDiedStaticEvent_OnEnemyDied(EnemyDiedStaticEventArgs enemyDiedStaticEventArgs)
    {
        if (enemyDiedStaticEventArgs.HasDiedOutOfHeadshot)
        {
            InstantiateDeathText("Headshot", enemyDiedStaticEventArgs.DiedPosition + Vector3.up);
            return;
        }

        InstantiateDeathText("Kill!", enemyDiedStaticEventArgs.DiedPosition + Vector3.up);
    }

    private void InstantiateDeathText(string actionText, Vector3 worldPosition)
    {
        ActionText deathText = Instantiate(_actionTextPrefab, transform);
        deathText.Initialize(actionText, worldPosition);
    }

    private void InstantiateScoreCubeText(string actionText, Vector3 worldPosition)
    {
        ActionText scoreCubeText = Instantiate(_actionTextPrefab, transform);
        scoreCubeText.Initialize(actionText, worldPosition);
    }
}
