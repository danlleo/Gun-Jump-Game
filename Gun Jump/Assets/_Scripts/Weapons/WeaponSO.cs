using UnityEngine;

namespace _Scripts.Weapons
{
    [CreateAssetMenu(fileName = "Weapon_", menuName = "Scriptable Objects/Weapons/Weapon")]
    public class WeaponSO : ScriptableObject
    {
        [Space(10)]
        [Header("Weapon")]
        [Space(5)]

        #region VALUES THAT ARE ASSIGNED IN THE INSPECTOR

        [SerializeField] private string _weaponName;
        [SerializeField] private string _weaponID;
        [SerializeField] private int _requiredLevelToUnlock;
        [SerializeField] private int _priceToUnlock;

        [SerializeField] private WeaponDetailsSO _weaponDetails;
        [SerializeField] private Weapon _weaponPrefab;

        #endregion

        #region REFERENCE VALUES

        public string WeaponName => _weaponName;
        public string WeaponID => _weaponID;
        public int RequiredLevelToUnlock => _requiredLevelToUnlock;
        public int PriceToUnlock => _priceToUnlock;

        public WeaponDetailsSO WeaponDetails => _weaponDetails;
        public Weapon WeaponPrefab => _weaponPrefab;

        #endregion
    }
}
