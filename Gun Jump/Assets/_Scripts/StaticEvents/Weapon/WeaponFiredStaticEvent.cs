using System;

public static class WeaponFiredStaticEvent
{
    public static event Action OnWeaponFired;

    public static void CallWeaponFiredEvent()
        => OnWeaponFired?.Invoke();
}
