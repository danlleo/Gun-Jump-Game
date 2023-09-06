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
        WeaponFallingStaticEvent.OnWeaponFalling += WeaponFallingStaticEvent_OnWeaponFalling;
    }

    private void OnDisable()
    {
        WeaponFiredStaticEvent.OnWeaponFired -= WeaponFiredStaticEvent_OnWeaponFired;
        WeaponFallingStaticEvent.OnWeaponFalling -= WeaponFallingStaticEvent_OnWeaponFalling;
    }

    private void WeaponFallingStaticEvent_OnWeaponFalling()
    {
        _virtualCamera.Follow = null;
        _virtualCamera.LookAt = null;
    }

    private void WeaponFiredStaticEvent_OnWeaponFired(WeaponFiredEventArgs _)
    {
        _virtualCamera.Follow = SelectedWeapon.Instance.GetSelectedWeapon().transform;
        _virtualCamera.LookAt = SelectedWeapon.Instance.GetSelectedWeapon().transform;

        WeaponFiredStaticEvent.OnWeaponFired -= WeaponFiredStaticEvent_OnWeaponFired;
    }
}
