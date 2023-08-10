using UnityEngine;

[System.Serializable]
public struct WeaponProperties
{
    public float MaxAngularVelocity;
    public float Torque;
    public float MaxJumpHeight;
    public float JumpForce;
    public float BounceBackForce;
    public Transform ProjectileSpawnPoint;
}
