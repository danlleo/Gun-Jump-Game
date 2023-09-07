using System;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class SelectedWeaponChangeEvent : MonoBehaviour
    {
        public Action<SelectedWeaponChangeEvent, SelectedWeaponChangeEventArgs> OnSelectedWeaponChange;

        public void CallSelectedWeaponChangeEvent(WeaponSO selectedWeapon)
            => OnSelectedWeaponChange?.Invoke(this, new SelectedWeaponChangeEventArgs(selectedWeapon));
    }

    public class SelectedWeaponChangeEventArgs : EventArgs
    {
        public WeaponSO SelectedWeapon;

        public SelectedWeaponChangeEventArgs(WeaponSO selectedWeapon)
        {
            SelectedWeapon = selectedWeapon;
        }
    }
}