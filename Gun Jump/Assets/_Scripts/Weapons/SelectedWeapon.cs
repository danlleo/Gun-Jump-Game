using UnityEngine;

public class SelectedWeapon : Singleton<SelectedWeapon>
{
    [SerializeField] private Weapon _selectedWeapon;
    [SerializeField] private Transform _weaponSpawnTransform;

    [SerializeField] private float _minimumHeightValueAllowed;

    protected override void Awake()
    {
        base.Awake();
        _selectedWeapon.transform.position = _weaponSpawnTransform.position;
    }

    private void Update()
    {
        if (_selectedWeapon.transform.position.y < _minimumHeightValueAllowed)
        {
            // Call the event here, telling that we're falling
            WeaponFallingStaticEvent.CallWeaponFallingEvent();
            Destroy(this);
        }
    }

    public Weapon GetSelectedWeapon()
        => _selectedWeapon;

    public void SetSelectedWeapon(Weapon weaponToSelect)
    {
        foreach (Transform weaponTransform in transform)
        {
            if (weaponTransform.TryGetComponent(out Weapon weapon))
            {
                if (weapon.GetType() == weaponToSelect.GetType())
                {
                    _selectedWeapon = weapon;
                    weaponTransform.position = _weaponSpawnTransform.position;
                    weaponTransform.gameObject.SetActive(true);
                    continue;
                }

                weaponTransform.gameObject.SetActive(false);
            }
        }
    }
}
