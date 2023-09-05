using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_", menuName = "Scriptable Objects/Weapons/Weapon")]
public class WeaponSO : ScriptableObject
{
    [Space(10)]
    [Header("Weapon")]
    [Space(5)]

    public string WeaponName;
    public int RequiredLevelToUnlock;
    public int PriceToUnlock;
    public WeaponDetailsSO WeaponDetails;
    public Weapon WeaponPrefab;
}
