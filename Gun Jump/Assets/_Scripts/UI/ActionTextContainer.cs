using UnityEngine;

[DisallowMultipleComponent]
public class ActionTextContainer : MonoBehaviour
{
    [SerializeField] private ActionText _actionTextPrefab;

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
        if (enemyDiedStaticEventArgs.HasDiedOutOfHeadshot)
        {
            InstantiateDeathText("Headshot", enemyDiedStaticEventArgs.DiedPosition);
            return;
        }

        InstantiateDeathText("Kill!", enemyDiedStaticEventArgs.DiedPosition);
    }

    private void InstantiateDeathText(string actionText, Vector3 worldPosition)
    {
        ActionText deathText = Instantiate(_actionTextPrefab, transform);
        deathText.Initialize(actionText, worldPosition);
    }
}
