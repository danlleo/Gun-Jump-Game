using UnityEngine;

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
    }

    private void OnDisable()
    {
        _weaponStoreScrollToggleOpenEvent.OnToggleOpenChange -= WeaponStoreScrollToggleOpenEvent_OnToggleOpenChange;
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
