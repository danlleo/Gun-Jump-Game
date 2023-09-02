using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[DisallowMultipleComponent]
public abstract class Weapon : MonoBehaviour
{
    protected abstract Rigidbody RB { get; }
    protected abstract WeaponDetailsSO WeaponDetails { get; }
    protected abstract Transform WeaponProjectileSpawnPoint { get; }

    /// <summary>
    /// Fire a projectile from the weapon, implement differently in each sub-class
    /// </summary>
    protected virtual void Fire()
    {
        WeaponFiredStaticEvent.CallWeaponFiredEvent(WeaponProjectileSpawnPoint);
    }

    /// <summary>
    /// Bounces the weapon back according to the shot position
    /// </summary>
    protected virtual void BounceBack()
    {
        float heightMultiplier = Mathf.InverseLerp(WeaponDetails.MaxJumpHeight, 0, transform.position.y);

        Vector3 heightForce = Vector3.Dot(Vector3.up, -transform.forward) < 0
            ? Vector3.zero
            : WeaponDetails.JumpForce * heightMultiplier * Vector3.up;

        Vector3 sideForce = WeaponDetails.SideForce * Vector3.Dot(Vector3.forward, WeaponProjectileSpawnPoint.forward) * Vector3.forward;

        RB.velocity = Vector3.zero;
        RB.AddForce((-transform.forward + heightForce - sideForce) * WeaponDetails.BounceBackForce);
    }

    /// <summary>
    /// Applies rotation force depending on the shot position rotation
    /// </summary>
    protected virtual void ApplyTorque()
    {
        float angularPoint = Mathf.InverseLerp(0, WeaponDetails.MaxAngularVelocity, Mathf.Abs(RB.angularVelocity.x));
        float bonusAmount = Mathf.Lerp(0, WeaponDetails.MaxTorqueBonus, angularPoint);
        float torque = WeaponDetails.Torque + bonusAmount;

        Vector3 facingDirection = Vector3.Dot(WeaponProjectileSpawnPoint.forward, Vector3.forward) < 0
                ? Vector3.left
                : Vector3.right;

        RB.AddTorque(facingDirection * torque);
    }

    /// <summary>
    /// Sets limit to the rotation speed when applying rotation force
    /// </summary>
    protected virtual void ClampAngularVelocity()
    {
        if (RB == null)
            return;

        RB.angularVelocity = new Vector3(
            Mathf.Clamp(
                RB.angularVelocity.x,
                -WeaponDetails.MaxAngularVelocity,
                WeaponDetails.MaxAngularVelocity
            ),
            0f,
            0f
        );
    }
}
