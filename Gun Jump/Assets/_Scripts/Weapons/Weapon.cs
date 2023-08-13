using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Weapon : MonoBehaviour
{
    protected abstract Rigidbody RB { get; }
    protected abstract WeaponProperties Properties { get; }

    protected abstract void Fire();

    protected virtual void BounceBack()
    {
        float heightMultiplier = Mathf.InverseLerp(Properties.MaxJumpHeight, 0, transform.position.y);

        Vector3 heightForce = Vector3.Dot(Vector3.up, -transform.forward) < 0
            ? Vector3.zero
            : Properties.JumpForce * heightMultiplier * Vector3.up;

        Vector3 sideForce = Properties.SideForce * Vector3.Dot(Vector3.forward, Properties.ProjectileSpawnPoint.forward) * Vector3.forward;

        RB.velocity = Vector3.zero;
        RB.AddForce((-transform.forward + heightForce - sideForce) * Properties.BounceBackForce);
    }

    protected virtual void ApplyTorque()
    {
        Vector3 facingDirection = Vector3.Dot(Properties.ProjectileSpawnPoint.forward, Vector3.forward) < 0
                ? Vector3.left
                : Vector3.right;

        RB.AddTorque(facingDirection * Properties.Torque);
    }

    protected virtual void ClampAngularVelocity()
    {
        if (RB == null)
            return;

        RB.angularVelocity = new Vector3(
            Mathf.Clamp(
                RB.angularVelocity.x,
                -Properties.MaxAngularVelocity,
                Properties.MaxAngularVelocity
            ),
            0f,
            0f
        );
    }
}
