using _Scripts.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.WeaponStoreScroll
{
    [RequireComponent(typeof(WeaponStoreScrollToggleOpenEvent))]
    [RequireComponent(typeof(SelectedWeaponChangeEvent))]
    [DisallowMultipleComponent]
    public class WeaponStoreScroll : MonoBehaviour
    {
        [HideInInspector] public WeaponStoreScrollToggleOpenEvent WeaponStoreScrollToggleOpenEvent;
        [HideInInspector] public SelectedWeaponChangeEvent SelectedWeaponChangeEvent;
            
        public bool IsOpen { private set; get; }

        [SerializeField] private Button _closeButton;
        [SerializeField] private TextMeshProUGUI _weaponDisplayNameText;
        
        private void Awake()
        {
            WeaponStoreScrollToggleOpenEvent = GetComponent<WeaponStoreScrollToggleOpenEvent>();
            SelectedWeaponChangeEvent = GetComponent<SelectedWeaponChangeEvent>();

            _closeButton.onClick.AddListener(() =>
            {
                WeaponStoreScrollToggleOpenEvent.CallToggleOpenChange(false);
            });
        }

        private void Start()
        {
            UpdateWeaponDisplayNameText(SelectedWeapon.Instance.GetSelectedWeapon().name);
        }

        private void OnEnable()
        {
            WeaponStoreScrollToggleOpenEvent.OnToggleOpenChange += WeaponStoreScrollToggleOpenEvent_OnToggleOpenChange;
            SelectedWeaponChangeEvent.OnSelectedWeaponChange += SelectedWeaponChangeEvent_OnSelectedWeaponChange;
        }


        private void OnDisable()
        {
            WeaponStoreScrollToggleOpenEvent.OnToggleOpenChange -= WeaponStoreScrollToggleOpenEvent_OnToggleOpenChange;
            SelectedWeaponChangeEvent.OnSelectedWeaponChange -= SelectedWeaponChangeEvent_OnSelectedWeaponChange;
        }

        private void SelectedWeaponChangeEvent_OnSelectedWeaponChange(SelectedWeaponChangeEvent selectedWeaponChangeEvent, SelectedWeaponChangeEventArgs selectedWeaponChangeEventArgs)
        {
            UpdateWeaponDisplayNameText(selectedWeaponChangeEventArgs.SelectedWeapon.WeaponName);
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

        private void UpdateWeaponDisplayNameText(string weaponName)
            => _weaponDisplayNameText.text = $"{weaponName}";
    }
}
