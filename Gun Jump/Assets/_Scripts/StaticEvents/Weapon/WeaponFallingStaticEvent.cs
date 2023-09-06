using System;

namespace _Scripts.StaticEvents.Weapon
{
    public static class WeaponFallingStaticEvent
    {
        public static event Action OnWeaponFalling;

        public static void CallWeaponFallingEvent()
            => OnWeaponFalling?.Invoke();
    }
}
