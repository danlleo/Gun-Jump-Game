using System;
using UnityEngine;

namespace _Scripts.StaticEvents.Weapon
{
    public static class WeaponFiredStaticEvent
    {
        public static event Action<WeaponFiredEventArgs> OnWeaponFired;

        public static void CallWeaponFiredEvent(Transform firedWeaponShootTransform)
            => OnWeaponFired?.Invoke(new WeaponFiredEventArgs(firedWeaponShootTransform));
    }

    public class WeaponFiredEventArgs : EventArgs
    {
        public Transform FiredWeaponShootTransform;

        public WeaponFiredEventArgs(Transform firedWeaponShootTransform)
        {
            FiredWeaponShootTransform = firedWeaponShootTransform;
        }
    }
}