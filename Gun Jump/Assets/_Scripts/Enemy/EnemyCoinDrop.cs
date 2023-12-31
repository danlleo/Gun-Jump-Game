using _Scripts.Coin;
using _Scripts.Weapons;
using UnityEngine;

namespace _Scripts.Enemy
{
    [RequireComponent(typeof(EnemyHitEvent))]
    [DisallowMultipleComponent]
    public class EnemyCoinDrop : MonoBehaviour
    {
        private const int COINS_SPAWN_AMOUNT = 7;

        private EnemyHitEvent _enemyHitEvent;

        private void Awake()
        {
            _enemyHitEvent = GetComponent<EnemyHitEvent>();
        }

        private void OnEnable()
        {
            _enemyHitEvent.OnEnemyHit += EnemyHitEvent_OnEnemyHit;
        }

        private void OnDisable()
        {
            _enemyHitEvent.OnEnemyHit -= EnemyHitEvent_OnEnemyHit;
        }

        private void EnemyHitEvent_OnEnemyHit(EnemyHitEvent enemyHitEvent, EnemyHitEventArgs enemyHitEventArgs)
        {
            SpawnCoins(enemyHitEvent.gameObject.transform.position, SelectedWeapon.Instance.GetSelectedWeapon().transform);
        }

        private void SpawnCoins(Vector3 spawnPosition, Transform objectToFollow)
        {
            for (int i = 0; i < COINS_SPAWN_AMOUNT; i++)
            {
                var coin = CoinPool.Instance.GetPooledObject();

                coin.Initialize(spawnPosition, Quaternion.identity, objectToFollow);
                coin.ApplyRandomForceToCoin();
            }
        }
    }
}
