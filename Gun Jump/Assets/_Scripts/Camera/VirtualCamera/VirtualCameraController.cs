using Cinemachine;
using UnityEngine;

[DisallowMultipleComponent]
public class VirtualCameraController : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        WeaponFiredStaticEvent.OnWeaponFired += WeaponFiredStaticEvent_OnWeaponFired;
    }

    private void OnDisable()
    {
        WeaponFiredStaticEvent.OnWeaponFired -= WeaponFiredStaticEvent_OnWeaponFired;
    }

    private void WeaponFiredStaticEvent_OnWeaponFired(WeaponFiredEventArgs _)
    {
        _virtualCamera.Follow = SelectedWeapon.Instance.GetSelectedWeaponTransform();
        _virtualCamera.LookAt = SelectedWeapon.Instance.GetSelectedWeaponTransform();

        WeaponFiredStaticEvent.OnWeaponFired -= WeaponFiredStaticEvent_OnWeaponFired;
    }
}
