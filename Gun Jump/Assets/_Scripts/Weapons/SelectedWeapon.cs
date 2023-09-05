using UnityEngine;

public class SelectedWeapon : Singleton<SelectedWeapon>
{
    [SerializeField] private Transform _selectedWeapon;

    protected override void Awake()
    {
        base.Awake();
    }

    public Transform GetSelectedWeaponTransform()
        => _selectedWeapon;

    
}
