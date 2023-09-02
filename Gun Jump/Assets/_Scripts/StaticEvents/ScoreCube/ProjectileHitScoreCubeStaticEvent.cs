using UnityEngine;
using System;

public static class ProjectileHitScoreCubeStaticEvent
{
    public static event Action<ProjectileHitScoreCubeEventArgs> OnProjectileHitScoreCube;

    public static void CallProjectileHitScoreCubeEvent(int moneyMultiplierAmount, Vector3 worldPosition, bool canDestroy)
        => OnProjectileHitScoreCube?.Invoke(new ProjectileHitScoreCubeEventArgs(moneyMultiplierAmount, worldPosition, canDestroy));
}

public class ProjectileHitScoreCubeEventArgs : EventArgs
{
    public int MoneyMultiplierAmount;
    public Vector3 WorldPosition;
    public bool CanDestroy;

    public ProjectileHitScoreCubeEventArgs(int moneyMultiplierAmount, Vector3 worldPosition, bool canDestroy)
    {
        MoneyMultiplierAmount = moneyMultiplierAmount;
        WorldPosition = worldPosition;
        CanDestroy = canDestroy;
    }
}
