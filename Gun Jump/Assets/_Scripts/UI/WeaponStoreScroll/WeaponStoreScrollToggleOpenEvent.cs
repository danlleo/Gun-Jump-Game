using System;
using UnityEngine;

namespace _Scripts.UI.WeaponStoreScroll
{
    [DisallowMultipleComponent]
    public class WeaponStoreScrollToggleOpenEvent : MonoBehaviour
    {
        public event Action<WeaponStoreScrollToggleOpenEventArgs> OnToggleOpenChange;

        public void CallToggleOpenChange(bool isOpen)
            => OnToggleOpenChange?.Invoke(new WeaponStoreScrollToggleOpenEventArgs(isOpen));
    }

    public class WeaponStoreScrollToggleOpenEventArgs : EventArgs
    {
        public bool IsOpen;

        public WeaponStoreScrollToggleOpenEventArgs(bool isOpen)
        {
            IsOpen = isOpen;
        }
    }
}