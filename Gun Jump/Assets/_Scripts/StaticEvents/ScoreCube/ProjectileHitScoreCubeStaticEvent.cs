using System;

public static class ProjectileHitScoreCubeStaticEvent
{
    public static event Action OnProjectileHitScoreCube;

    public static void CallProjectileHitScoreCubeEvent()
        => OnProjectileHitScoreCube?.Invoke();
}
