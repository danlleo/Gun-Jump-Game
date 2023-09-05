using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class WeaponScrollSelectItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private WeaponSO _weaponSO;
    [SerializeField] private Color _availableBackgroundColor;
    [SerializeField] private Color _unavailableBackgroundColor;
    [SerializeField] private GameObject _notAvailableGameObject;
    [SerializeField] private Image _background;

    private bool _availableForPurchaseOrSelect;

    private void Start()
    {
        if (_weaponSO.RequiredLevelToUnlock >= GameManager.Instance.CurrentLevel)
            _availableForPurchaseOrSelect = true;

        ApplyStylesAccordingToAvailableState(_availableForPurchaseOrSelect);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // ...
    }

    private void ApplyStylesAccordingToAvailableState(bool availableForPurchaseOrSelect)
    {
        if (availableForPurchaseOrSelect)
            return;

        _background.color = _unavailableBackgroundColor;
        _notAvailableGameObject.SetActive(true);
    }
}
