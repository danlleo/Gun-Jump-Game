using _Scripts.StaticEvents.Economy;
using _Scripts.Utilities.GameManager;
using _Scripts.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Scripts.UI.WeaponScrollSelectItem
{
    [DisallowMultipleComponent]
    public class WeaponScrollSelectItem : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private WeaponSO _weaponSO;
        [SerializeField] private Color _availableBackgroundColor;
        [SerializeField] private Color _unavailableBackgroundColor;
        [SerializeField] private GameObject _notAvailableGameObject;
        [SerializeField] private GameObject _purchaseGameObject;
        [SerializeField] private TextMeshProUGUI _requiredLevelToUnlockText;
        [SerializeField] private TextMeshProUGUI _purchasePriceText;
        [SerializeField] private Image _background;

        private void Start()
        {
            ApplyStylesAccordingToAvailableState(IsAvailableForPurchaseOrSelect());
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!IsWeaponPurchased())
            {
                if (Economy.Economy.TryPurchaseWeapon(_weaponSO.PriceToUnlock) && IsAvailableForPurchaseOrSelect())
                {
                    GameManager.Instance.SaveGameData.PurchasedWeaponPrefabIDList.Add(_weaponSO.WeaponID);
                    EconomyMadePurchaseStaticEvent.CallMadePurchaseEvent();
                    ApplyStylesAccordingToAvailableState(IsAvailableForPurchaseOrSelect());
                }
            
                return;
            }

            SelectedWeapon.Instance.SetSelectedWeapon(_weaponSO.WeaponPrefab);
        }

        private void ApplyStylesAccordingToAvailableState(bool availableForPurchaseOrSelect)
        {
            if (availableForPurchaseOrSelect)
            {
                if (!IsWeaponPurchased())
                {
                    // Enable purchase styles
                    _purchaseGameObject.SetActive(true);
                    SetPurchasePriceText(_weaponSO.PriceToUnlock);
                }
                else
                    _purchaseGameObject.SetActive(false);
            
                return;
            }

            // Enable unavailable styles
            _background.color = _unavailableBackgroundColor;
            _notAvailableGameObject.SetActive(true);
            SetRequiredLevelToUnlockText(_weaponSO.RequiredLevelToUnlock);
        }

        private void SetRequiredLevelToUnlockText(int requiredLevel)
            => _requiredLevelToUnlockText.text = $"{requiredLevel}";

        private void SetPurchasePriceText(int price)
            => _purchasePriceText.text = $"{price}";

        private bool IsWeaponPurchased()
            => GameManager.Instance.SaveGameData.PurchasedWeaponPrefabIDList.Contains(_weaponSO.WeaponID);

        private bool IsAvailableForPurchaseOrSelect()
            => GameManager.Instance.CurrentLevel >= _weaponSO.RequiredLevelToUnlock;
    }
}
