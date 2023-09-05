using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(WeaponStoreScrollToggleOpenEvent))]
[DisallowMultipleComponent]
public class WeaponStoreScroll : MonoBehaviour
{
    [HideInInspector] public WeaponStoreScrollToggleOpenEvent WeaponStoreScrollToggleOpenEvent;

    [HideInInspector] public bool IsOpen { private set; get; }

    [SerializeField] private Button _closeButton;

    private void Awake()
    {
        WeaponStoreScrollToggleOpenEvent = GetComponent<WeaponStoreScrollToggleOpenEvent>();

        _closeButton.onClick.AddListener(() =>
        {
            WeaponStoreScrollToggleOpenEvent.CallToggleOpenChange(false);
        });
    }

    private void OnEnable()
    {
        WeaponStoreScrollToggleOpenEvent.OnToggleOpenChange += WeaponStoreScrollToggleOpenEvent_OnToggleOpenChange;
    }

    private void OnDisable()
    {
        WeaponStoreScrollToggleOpenEvent.OnToggleOpenChange -= WeaponStoreScrollToggleOpenEvent_OnToggleOpenChange;
    }

    private void WeaponStoreScrollToggleOpenEvent_OnToggleOpenChange(WeaponStoreScrollToggleOpenEventArgs weaponStoreScrollToggleOpenEventArgs)
    {
        if (weaponStoreScrollToggleOpenEventArgs.IsOpen)
        {
            IsOpen = true;
            return;
        }

        IsOpen = false;
    }
}
