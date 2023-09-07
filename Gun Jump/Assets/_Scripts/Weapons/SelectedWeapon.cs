using System;
using _Scripts.Misc;
using _Scripts.StaticEvents.Weapon;
using _Scripts.Utilities.GameManager;
using UnityEngine;

namespace _Scripts.Weapons
{
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

        private void Start()
        {
            SetSelectedWeapon(GameManager.Instance.SaveGameData.SelectedWeaponID);
        }

        private void Update()
        {
            if (!(_selectedWeapon.transform.position.y < _minimumHeightValueAllowed)) return;
            
            WeaponFallingStaticEvent.CallWeaponFallingEvent();
            Destroy(this);
        }

        public Weapon GetSelectedWeapon()
            => _selectedWeapon;

        public void SetSelectedWeapon(Weapon weaponToSelect)
        {
            foreach (Transform weaponTransform in transform)
            {
                if (!weaponTransform.TryGetComponent(out Weapon weapon)) continue;
                
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
        
        public void SetSelectedWeapon(string weaponToSelectID)
        {
            foreach (Transform weaponTransform in transform)
            {
                if (!weaponTransform.TryGetComponent(out Weapon weapon)) continue;
                
                if (weapon.WeaponSO.WeaponID == weaponToSelectID)
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
