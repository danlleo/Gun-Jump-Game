using System;

public static class WeaponFallingStaticEvent
{
    public static event Action OnWeaponFalling;

    public static void CallWeaponFallingEvent()
        => OnWeaponFalling?.Invoke();
}
