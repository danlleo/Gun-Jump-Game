using _Scripts.StaticEvents.Weapon;
using UnityEngine;

namespace _Scripts.UI.WeaponStoreScroll
{
    [RequireComponent(typeof(WeaponStoreScrollToggleOpenEvent))]
    [RequireComponent(typeof(Animator))]
    [DisallowMultipleComponent]
    public class WeaponStoreScrollAnimator : MonoBehaviour
    {
        private Animator _animator;
        private WeaponStoreScrollToggleOpenEvent _weaponStoreScrollToggleOpenEvent;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _weaponStoreScrollToggleOpenEvent = GetComponent<WeaponStoreScrollToggleOpenEvent>();
        }

        private void OnEnable()
        {
            _weaponStoreScrollToggleOpenEvent.OnToggleOpenChange += WeaponStoreScrollToggleOpenEvent_OnToggleOpenChange;
            WeaponFiredStaticEvent.OnWeaponFired += WeaponFiredStaticEvent_OnWeaponFired;
        }

        private void OnDisable()
        {
            _weaponStoreScrollToggleOpenEvent.OnToggleOpenChange -= WeaponStoreScrollToggleOpenEvent_OnToggleOpenChange;
            WeaponFiredStaticEvent.OnWeaponFired -= WeaponFiredStaticEvent_OnWeaponFired;
        }

        private void WeaponFiredStaticEvent_OnWeaponFired(WeaponFiredEventArgs _)
        {
            _animator.SetTrigger(WeaponStoreScrollAnimationParams.OnClose);
            WeaponFiredStaticEvent.OnWeaponFired -= WeaponFiredStaticEvent_OnWeaponFired;
        }

        private void WeaponStoreScrollToggleOpenEvent_OnToggleOpenChange(WeaponStoreScrollToggleOpenEventArgs weaponStoreScrollToggleOpenEventArgs)
        {
            if (weaponStoreScrollToggleOpenEventArgs.IsOpen)
            {
                _animator.SetTrigger(WeaponStoreScrollAnimationParams.OnOpen);
                return;
            }

            _animator.SetTrigger(WeaponStoreScrollAnimationParams.OnClose);
        }
    }
}
