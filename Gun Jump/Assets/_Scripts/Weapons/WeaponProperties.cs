using UnityEngine;

[System.Serializable]
public struct WeaponProperties
{
    public float MaxAngularVelocity;
    public float Torque;
    public float MaxTorqueBonus;
    public float MaxJumpHeight;
    public float JumpForce;
    public float SideForce;
    public float BounceBackForce;
    public bool ProjectilesCanRicochet;
    public Transform ProjectileSpawnPoint;
}
