using System;
using UnityEngine;

namespace _Scripts.StaticEvents.ScoreCube
{
    public static class ProjectileHitScoreCubeStaticEvent
    {
        public static event Action<ProjectileHitScoreCubeEventArgs> OnProjectileHitScoreCube;

        public static void CallProjectileHitScoreCubeEvent(int moneyMultiplierAmount, Vector3 scoreCubeWorldPosition, bool canDestroy)
            => OnProjectileHitScoreCube?.Invoke(new ProjectileHitScoreCubeEventArgs(moneyMultiplierAmount, scoreCubeWorldPosition, canDestroy));
    }

    public class ProjectileHitScoreCubeEventArgs : EventArgs
    {
        public int MoneyMultiplierAmount;
        public Vector3 ScoreCubeWorldPosition;
        public bool CanDestroy;

        public ProjectileHitScoreCubeEventArgs(int moneyMultiplierAmount, Vector3 scoreCubeWorldPosition, bool canDestroy)
        {
            MoneyMultiplierAmount = moneyMultiplierAmount;
            ScoreCubeWorldPosition = scoreCubeWorldPosition;
            CanDestroy = canDestroy;
        }
    }
}