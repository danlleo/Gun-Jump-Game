using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDetails_", menuName = "Scriptable Objects/Weapons/WeaponDetails")]
public class WeaponDetailsSO : ScriptableObject
{
    #region WEAPON PROPERTIES

    [Space(10)]
    [Header("Weapon Properties")]
    [Space(5)]

    public float MaxAngularVelocity;
    public float Torque;
    public float MaxTorqueBonus;
    public float MaxJumpHeight;
    public float JumpForce;
    public float SideForce;
    public float BounceBackForce;

    #endregion

    #region WEAPON'S PROJECTILE PROPERTIES

    [Space(10)]
    [Header("Weapon's Projectile Properties")]
    [Space(5)]

    public bool ProjectilesCanRicochet;
    public bool ProjectilesCanGoTroughBodies;

    #endregion;
}
