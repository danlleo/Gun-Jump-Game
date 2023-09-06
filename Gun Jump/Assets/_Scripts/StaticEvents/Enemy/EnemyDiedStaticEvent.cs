using System;
using UnityEngine;

namespace _Scripts.StaticEvents.Enemy
{
    public static class EnemyDiedStaticEvent
    {
        public static event Action<EnemyDiedStaticEventArgs> OnEnemyDied;

        public static void CallEnemyDiedEvent(Vector3 diedPosition, bool hasDiedOutOfHeadshot = false)
            => OnEnemyDied?.Invoke(new EnemyDiedStaticEventArgs(diedPosition, hasDiedOutOfHeadshot));
    }

    public class EnemyDiedStaticEventArgs : EventArgs
    {
        public bool HasDiedOutOfHeadshot;
        public Vector3 DiedPosition;

        public EnemyDiedStaticEventArgs(Vector3 diedPosition, bool hasDiedOutOfHeadshot = false)
        {
            HasDiedOutOfHeadshot = hasDiedOutOfHeadshot;
            DiedPosition = diedPosition;
        }
    }
}